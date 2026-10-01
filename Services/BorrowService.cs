using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Validators;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    /// <summary>
    /// The most important service — handles borrowing and returning.
    /// All inventory changes go through this service to ensure consistency.
    /// Uses database transactions so that partial failures are rolled back.
    /// </summary>
    public class BorrowService
    {
        private static readonly BorrowRequestValidator _validator = new();
        private readonly LibraryDbContext _context;

        public BorrowService(LibraryDbContext context)
        {
            _context = context;
        }

        // ── BORROW ────────────────────────────────────────────────────────────

        /// <summary>
        /// Creates a new borrow transaction with one or more books.
        /// Transaction actor is derived securely from the authenticated session (SessionManager.CurrentLibrarian).
        /// 
        /// Business rules:
        /// - Member must exist
        /// - Authenticated Librarian session must exist (cannot be spoofed from caller)
        /// - At least one book must be selected
        /// - Duplicate books in request are automatically merged
        /// - Each book must have sufficient available copies
        /// - Due date must be >= borrow date + 1 day (DueDate.Date > BorrowDate.Date)
        /// - Everything is saved atomically (all or nothing, rollback on failure)
        /// </summary>
        public (bool success, string message) CreateBorrow(
            int memberId,
            DateTime dueDate,
            List<(int bookId, int quantity)> items)
        {
            var currentLibrarian = Helpers.SessionManager.CurrentLibrarian;
            if (currentLibrarian == null)
            {
                return (false, "Authentication required. You must be logged in to process a borrow transaction.");
            }

            return CreateBorrowInternal(memberId, currentLibrarian.LibrarianId, dueDate, items);
        }

        /// <summary>
        /// Internal overload for automated testing when setting up specific test fixtures.
        /// Production UI operations must always call the authenticated 3-parameter overload.
        /// </summary>
        internal (bool success, string message) CreateBorrow(
            int memberId,
            int librarianId,
            DateTime dueDate,
            List<(int bookId, int quantity)> items)
        {
            return CreateBorrowInternal(memberId, librarianId, dueDate, items);
        }

        private (bool success, string message) CreateBorrowInternal(
            int memberId,
            int librarianId,
            DateTime dueDate,
            List<(int bookId, int quantity)> items)
        {
            if (items == null || items.Count == 0)
                return (false, "Please select at least one book.");

            // Requirement 14: Deterministic service-layer duplicate book normalization
            // If the same Book is selected multiple times (e.g. Book A × 2, Book A × 3),
            // automatically merge them into (Book A × 5) before executing transaction.
            var normalizedItems = items
                .GroupBy(i => i.bookId)
                .Select(g => (bookId: g.Key, quantity: g.Sum(i => i.quantity)))
                .ToList();

            var request = new BorrowRequest(memberId, librarianId, dueDate, normalizedItems);
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return (false, validationResult.Errors.First().ErrorMessage);

            // Requirement 22: DueDate must be at least 1 day after BorrowDate (DateTime.Today)
            if (dueDate.Date <= DateTime.Today)
                return (false, "Due date must be at least one day after borrow date.");

            // Validate member in database
            var member = _context.Members.Find(memberId);
            if (member == null) return (false, "Member not found.");

            // Validate librarian in database
            var librarian = _context.Librarians.Find(librarianId);
            if (librarian == null) return (false, "Librarian not found.");

            // Atomic transaction for all borrow operations
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Pre-validate all books in live database before making any changes
                foreach (var (bookId, quantity) in normalizedItems)
                {
                    if (quantity <= 0)
                    {
                        transaction.Rollback();
                        return (false, "Quantity must be at least 1.");
                    }

                    var book = _context.Books.Find(bookId);
                    if (book == null)
                    {
                        transaction.Rollback();
                        return (false, $"Book ID {bookId} not found.");
                    }

                    if (quantity > book.AvailableCopies)
                    {
                        transaction.Rollback();
                        return (false, $"'{book.Title}' has only {book.AvailableCopies} available copies. Requested: {quantity}.");
                    }
                }

                var borrow = new Borrow
                {
                    MemberId = memberId,
                    LibrarianId = librarianId,
                    BorrowDate = DateTime.Today,
                    DueDate = dueDate.Date,
                    Status = BorrowStatus.Active
                };
                _context.Borrows.Add(borrow);
                _context.SaveChanges(); // Get BorrowId

                foreach (var (bookId, quantity) in normalizedItems)
                {
                    var book = _context.Books.Find(bookId)!;
                    book.AvailableCopies -= quantity;

                    if (book.AvailableCopies < 0)
                    {
                        transaction.Rollback();
                        return (false, $"Inventory integrity violation: Available copies for '{book.Title}' cannot be negative.");
                    }

                    _context.BorrowDetails.Add(new BorrowDetail
                    {
                        BorrowId = borrow.BorrowId,
                        BookId = bookId,
                        Quantity = quantity
                    });
                }

                _context.SaveChanges();
                transaction.Commit();

                return (true, $"Books borrowed successfully. Borrow ID: {borrow.BorrowId}");
            }
            catch (DbUpdateConcurrencyException)
            {
                transaction.Rollback();
                return (false, "This book's availability was just changed by another action. Please refresh and try again.");
            }
            catch (DbUpdateException)
            {
                transaction.Rollback();
                return (false, "A database error occurred while creating the borrow record. Please try again.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BorrowService.CreateBorrow] {ex}");
                transaction.Rollback();
                return (false, "Unable to complete the borrow transaction. Please try again.");
            }
        }

        // ── RETURN ────────────────────────────────────────────────────────────

        /// <summary>
        /// Processes the return of a borrow transaction.
        /// 
        /// Business rules:
        /// - Borrow must exist and be Active/Overdue
        /// - If already returned: REJECT immediately
        /// - Calculates fine based on overdue days × 2000 KHR (FinePolicy)
        /// - If AvailableCopies + ReturnedQuantity > TotalCopies:
        ///     DO NOT clamp silently. REJECT, ROLLBACK, REPORT DATA INTEGRITY ERROR.
        /// - Restores available copies for each book
        /// - Sets status to Returned and records ReturnDate and FineAmount
        /// - All changes are atomic (commit together or rollback)
        /// </summary>
        public (bool success, string message, decimal fine) ProcessReturn(int borrowId)
        {
            var borrow = _context.Borrows
                .Include(b => b.BorrowDetails)
                    .ThenInclude(bd => bd.Book)
                .Include(b => b.Member)
                .FirstOrDefault(b => b.BorrowId == borrowId);

            if (borrow == null)
                return (false, "Borrow record not found.", 0m);

            if (borrow.Status == BorrowStatus.Returned)
                return (false, $"Borrow #{borrowId} has already been returned.", 0m);

            var returnDate = DateTime.Today;
            var fine = FinePolicy.CalculateFine(borrow.DueDate, returnDate);

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Restore book copies with strict data integrity validation
                foreach (var detail in borrow.BorrowDetails)
                {
                    if (detail.Book == null)
                    {
                        transaction.Rollback();
                        return (false, $"Data integrity error: Book record #{detail.BookId} is missing.", 0m);
                    }

                    // Requirement 20: If AvailableCopies + ReturnedQuantity > TotalCopies,
                    // DO NOT automatically clamp the value! DO NOT silently do AvailableCopies = TotalCopies!
                    // REJECT, ROLLBACK, and REPORT DATA-INTEGRITY ERROR.
                    if (detail.Book.AvailableCopies + detail.Quantity > detail.Book.TotalCopies)
                    {
                        transaction.Rollback();
                        return (false, $"Data integrity error: Returning {detail.Quantity} copies of '{detail.Book.Title}' would exceed Total Copies ({detail.Book.TotalCopies}). Available: {detail.Book.AvailableCopies}.", 0m);
                    }

                    detail.Book.AvailableCopies += detail.Quantity;
                }

                borrow.ReturnDate = returnDate;
                borrow.FineAmount = fine;
                borrow.Status = BorrowStatus.Returned;

                _context.SaveChanges();
                transaction.Commit();

                string msg = fine > 0
                    ? $"Return processed. Fine: {fine:N0} ៛ ({(returnDate - borrow.DueDate).Days} days overdue)"
                    : "Return processed. No fine — returned on time.";

                return (true, msg, fine);
            }
            catch (DbUpdateConcurrencyException)
            {
                transaction.Rollback();
                return (false, "This borrow or book was modified by another user. Please refresh and try again.", 0m);
            }
            catch (DbUpdateException)
            {
                transaction.Rollback();
                return (false, "A database error occurred while processing the return. Please try again.", 0m);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BorrowService.ProcessReturn] {ex}");
                transaction.Rollback();
                return (false, "Unable to complete the return process. Please try again.", 0m);
            }
        }

        // ── QUERIES ───────────────────────────────────────────────────────────

        public List<Borrow> GetActiveBorrows()
        {
            return _context.Borrows
                .Include(b => b.Member)
                .Include(b => b.Librarian)
                .Include(b => b.BorrowDetails).ThenInclude(bd => bd.Book)
                .Where(b => b.Status == BorrowStatus.Active || b.Status == BorrowStatus.Overdue)
                .OrderByDescending(b => b.BorrowDate)
                .ToList();
        }

        public Borrow? GetBorrowById(int borrowId)
        {
            return _context.Borrows
                .Include(b => b.Member)
                .Include(b => b.Librarian)
                .Include(b => b.BorrowDetails).ThenInclude(bd => bd.Book)
                .FirstOrDefault(b => b.BorrowId == borrowId);
        }

        /// <summary>
        /// Updates overdue status for borrows that passed their due date.
        /// Call this on application startup or periodically.
        /// </summary>
        public void UpdateOverdueStatuses()
        {
            var overdueItems = _context.Borrows
                .Where(b => b.Status == BorrowStatus.Active && b.DueDate.Date < DateTime.Today)
                .ToList();

            foreach (var borrow in overdueItems)
                borrow.Status = BorrowStatus.Overdue;

            if (overdueItems.Count > 0)
                _context.SaveChanges();
        }
    }
}

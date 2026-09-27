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
        /// 
        /// Business rules:
        /// - Member must exist
        /// - Librarian must be authenticated
        /// - At least one book must be selected
        /// - Each book must have sufficient available copies
        /// - Due date must be >= borrow date
        /// - Everything is saved atomically (all or nothing)
        /// </summary>
        public (bool success, string message) CreateBorrow(
            int memberId,
            int librarianId,
            DateTime dueDate,
            List<(int bookId, int quantity)> items)
        {
            var request = new BorrowRequest(memberId, librarianId, dueDate, items);
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return (false, validationResult.Errors.First().ErrorMessage);

            // Validate member in database
            var member = _context.Members.Find(memberId);
            if (member == null) return (false, "Member not found.");

            // Validate librarian in database
            var librarian = _context.Librarians.Find(librarianId);
            if (librarian == null) return (false, "Librarian not found.");

            // Pre-validate all books before touching anything
            foreach (var (bookId, quantity) in items)
            {
                var book = _context.Books.Find(bookId);
                if (book == null)
                    return (false, $"Book ID {bookId} not found.");
                if (quantity > book.AvailableCopies)
                    return (false, $"'{book.Title}' has only {book.AvailableCopies} available copies. Requested: {quantity}.");
            }

            // All validation passed — use a transaction for atomicity
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var borrow = new Borrow
                {
                    MemberId = memberId,
                    LibrarianId = librarianId,
                    BorrowDate = DateTime.Today,
                    DueDate = dueDate,
                    Status = BorrowStatus.Active
                };
                _context.Borrows.Add(borrow);
                _context.SaveChanges(); // Get BorrowId

                foreach (var (bookId, quantity) in items)
                {
                    var book = _context.Books.Find(bookId)!;
                    book.AvailableCopies -= quantity;

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
            catch (Exception ex)
            {
                transaction.Rollback();
                return (false, $"Failed to create borrow: {ex.Message}");
            }
        }

        // ── RETURN ────────────────────────────────────────────────────────────

        /// <summary>
        /// Processes the return of a borrow transaction.
        /// 
        /// Business rules:
        /// - Borrow must exist and be Active/Overdue
        /// - Calculates fine based on overdue days × 2000 KHR
        /// - Restores available copies for each book
        /// - Sets status to Returned
        /// - All changes are atomic
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
                // Restore book copies
                foreach (var detail in borrow.BorrowDetails)
                {
                    detail.Book!.AvailableCopies += detail.Quantity;
                    // Safety: never exceed total copies
                    if (detail.Book.AvailableCopies > detail.Book.TotalCopies)
                        detail.Book.AvailableCopies = detail.Book.TotalCopies;
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
                return (false, "This book's availability was just changed by another action. Please refresh and try again.", 0m);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return (false, $"Failed to process return: {ex.Message}", 0m);
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

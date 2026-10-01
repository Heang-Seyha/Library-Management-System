using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Validators;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    /// <summary>
    /// Handles all book-related business operations.
    /// </summary>
    public class BookService
    {
        private static readonly BookValidator _validator = new();
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAll()
        {
            return _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .OrderBy(b => b.Title)
                .ToList();
        }

        public List<Book> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return GetAll();

            var q = query.Trim();
            return _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Where(b => b.Title.Contains(q)
                         || b.ISBN.Contains(q)
                         || (b.Author != null && b.Author.Name.Contains(q))
                         || (b.Category != null && b.Category.Name.Contains(q)))
                .OrderBy(b => b.Title)
                .ToList();
        }

        public Book? GetById(int id)
        {
            return _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefault(b => b.BookId == id);
        }

        public (bool success, string message) Add(Book book)
        {
            var result = _validator.Validate(book);
            if (!result.IsValid)
                return (false, result.Errors.First().ErrorMessage);

            var trimmedIsbn = book.ISBN.Trim();
            if (_context.Books.Any(b => b.ISBN == trimmedIsbn))
                return (false, $"A book with ISBN '{book.ISBN}' already exists.");

            // Verify foreign keys exist in database
            if (!_context.Categories.Any(c => c.CategoryId == book.CategoryId))
                return (false, "Selected category does not exist.");

            if (!_context.Authors.Any(a => a.AuthorId == book.AuthorId))
                return (false, "Selected author does not exist.");

            if (!_context.Publishers.Any(p => p.PublisherId == book.PublisherId))
                return (false, "Selected publisher does not exist.");

            book.ISBN = trimmedIsbn;
            book.Title = book.Title.Trim();
            _context.Books.Add(book);
            _context.SaveChanges();
            return (true, "Book added successfully.");
        }

        public (bool success, string message) Update(Book book)
        {
            var result = _validator.Validate(book);
            if (!result.IsValid)
                return (false, result.Errors.First().ErrorMessage);

            var trimmedIsbn = book.ISBN.Trim();
            if (_context.Books.Any(b => b.ISBN == trimmedIsbn && b.BookId != book.BookId))
                return (false, $"Another book with ISBN '{book.ISBN}' already exists.");

            var existing = _context.Books.Find(book.BookId);
            if (existing == null) return (false, "Book not found.");

            // Verify foreign keys exist in database
            if (!_context.Categories.Any(c => c.CategoryId == book.CategoryId))
                return (false, "Selected category does not exist.");

            if (!_context.Authors.Any(a => a.AuthorId == book.AuthorId))
                return (false, "Selected author does not exist.");

            if (!_context.Publishers.Any(p => p.PublisherId == book.PublisherId))
                return (false, "Selected publisher does not exist.");

            int currentlyBorrowed = _context.BorrowDetails
                .Where(bd => bd.BookId == book.BookId && bd.Borrow != null && (bd.Borrow.Status == BorrowStatus.Active || bd.Borrow.Status == BorrowStatus.Overdue))
                .Sum(bd => (int?)bd.Quantity) ?? 0;

            if (book.TotalCopies < currentlyBorrowed)
            {
                return (false, $"Cannot reduce total copies to {book.TotalCopies}. There are currently {currentlyBorrowed} copies on loan.");
            }

            if (book.AvailableCopies + currentlyBorrowed > book.TotalCopies)
            {
                return (false, $"Available copies ({book.AvailableCopies}) plus currently borrowed copies ({currentlyBorrowed}) cannot exceed total copies ({book.TotalCopies}).");
            }

            // Concurrency handling: If caller provided RowVersion, ensure EF Core compares against it
            if (book.RowVersion != null && book.RowVersion.Length > 0)
            {
                _context.Entry(existing).OriginalValues[nameof(Book.RowVersion)] = book.RowVersion;
            }

            existing.Title = book.Title.Trim();
            existing.ISBN = trimmedIsbn;
            existing.Year = book.Year;
            existing.TotalCopies = book.TotalCopies;
            existing.AvailableCopies = book.AvailableCopies;
            existing.CategoryId = book.CategoryId;
            existing.AuthorId = book.AuthorId;
            existing.PublisherId = book.PublisherId;

            try
            {
                _context.SaveChanges();
                return (true, "Book updated successfully.");
            }
            catch (DbUpdateConcurrencyException)
            {
                return (false, "This book was modified by another action. Please refresh and try again.");
            }
        }

        public (bool success, string message) Delete(int bookId)
        {
            var book = _context.Books.Find(bookId);
            if (book == null) return (false, "Book not found.");

            bool hasHistory = _context.BorrowDetails.Any(bd => bd.BookId == bookId);
            if (hasHistory)
                return (false, "Cannot delete this book because it has borrowing history. Consider reducing total copies instead.");

            try
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return (true, "Book deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return (false, "Cannot delete this book because it is referenced by existing transactions or records.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BookService.Delete] {ex}");
                return (false, "An error occurred while deleting the book. Please try again.");
            }
        }
    }
}

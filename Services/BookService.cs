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
            var q = query.Trim().ToLower();
            return _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Where(b => b.Title.ToLower().Contains(q)
                         || b.ISBN.ToLower().Contains(q)
                         || b.Author!.Name.ToLower().Contains(q)
                         || b.Category!.Name.ToLower().Contains(q))
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

            if (_context.Books.Any(b => b.ISBN == book.ISBN.Trim()))
                return (false, $"A book with ISBN '{book.ISBN}' already exists.");

            book.ISBN = book.ISBN.Trim();
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

            if (_context.Books.Any(b => b.ISBN == book.ISBN.Trim() && b.BookId != book.BookId))
                return (false, $"Another book with ISBN '{book.ISBN}' already exists.");

            var existing = _context.Books.Find(book.BookId);
            if (existing == null) return (false, "Book not found.");

            existing.Title = book.Title.Trim();
            existing.ISBN = book.ISBN.Trim();
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

            _context.Books.Remove(book);
            _context.SaveChanges();
            return (true, "Book deleted successfully.");
        }
    }
}

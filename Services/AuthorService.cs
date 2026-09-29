using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Validators;

namespace LibraryManagementSystem.Services
{
    /// <summary>
    /// Service for Author master data management.
    /// </summary>
    public class AuthorService : CrudService<Author>
    {
        private static readonly AuthorValidator _validator = new();

        public AuthorService(LibraryDbContext context) : base(context) { }

        public override List<Author> GetAll() =>
            _context.Authors.OrderBy(a => a.Name).ToList();

        public (bool success, string message) Add(Author author)
        {
            var result = _validator.Validate(author);
            if (!result.IsValid)
                return (false, result.Errors.First().ErrorMessage);

            author.Name = author.Name.Trim();
            author.Gender = string.IsNullOrWhiteSpace(author.Gender) ? "Male" : author.Gender.Trim();
            _context.Authors.Add(author);
            _context.SaveChanges();
            return (true, "Author added successfully.");
        }

        public (bool success, string message) Update(Author author)
        {
            var result = _validator.Validate(author);
            if (!result.IsValid)
                return (false, result.Errors.First().ErrorMessage);

            var existing = _context.Authors.Find(author.AuthorId);
            if (existing == null) return (false, "Author not found.");
            existing.Name = author.Name.Trim();
            existing.Gender = string.IsNullOrWhiteSpace(author.Gender) ? "Male" : author.Gender.Trim();
            existing.DateOfBirth = author.DateOfBirth;
            existing.Bio = author.Bio?.Trim() ?? "";
            _context.SaveChanges();
            return (true, "Author updated successfully.");
        }

        public (bool success, string message) Delete(int authorId)
        {
            if (_context.Books.Any(b => b.AuthorId == authorId))
                return (false, "Cannot delete: books are assigned to this author.");
            var author = _context.Authors.Find(authorId);
            if (author == null) return (false, "Author not found.");
            try
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
                return (true, "Author deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Cannot delete author: {ex.Message}");
            }
        }
    }
}

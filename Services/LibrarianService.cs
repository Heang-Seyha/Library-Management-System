using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Validators;

namespace LibraryManagementSystem.Services
{
    /// <summary>
    /// Service for Librarian credential and account management.
    /// Handles account retrieval, creation with password hashing, updates, and safe deletion.
    /// </summary>
    public class LibrarianService : CrudService<Librarian>
    {
        private static readonly LibrarianValidator _validator = new();
        private static readonly PasswordValidator _passwordValidator = new();

        public LibrarianService(LibraryDbContext context) : base(context) { }

        public override List<Librarian> GetAll() =>
            _context.Librarians.OrderBy(l => l.Name).ToList();

        public override Librarian? GetById(int id) =>
            _context.Librarians.Find(id);

        public (bool success, string message) Add(Librarian librarian, string plainPassword)
        {
            var valResult = _validator.Validate(librarian);
            if (!valResult.IsValid)
                return (false, valResult.Errors.First().ErrorMessage);

            if (string.IsNullOrWhiteSpace(plainPassword))
                return (false, "Password is required.");

            var pwdResult = _passwordValidator.Validate(plainPassword);
            if (!pwdResult.IsValid)
                return (false, pwdResult.Errors.First().ErrorMessage);

            if (_context.Librarians.Any(l => l.Username.ToLower() == librarian.Username.Trim().ToLower()))
                return (false, $"Username '{librarian.Username}' is already taken.");

            librarian.Username = librarian.Username.Trim();
            librarian.PasswordHash = Helpers.PasswordHasher.Hash(plainPassword);
            _context.Librarians.Add(librarian);
            _context.SaveChanges();
            return (true, "Librarian added successfully.");
        }

        public (bool success, string message) Update(Librarian librarian, string? newPassword)
        {
            var existing = _context.Librarians.Find(librarian.LibrarianId);
            if (existing == null) return (false, "Librarian not found.");

            var valResult = _validator.Validate(librarian);
            if (!valResult.IsValid)
                return (false, valResult.Errors.First().ErrorMessage);

            if (_context.Librarians.Any(l => l.Username.ToLower() == librarian.Username.Trim().ToLower()
                                         && l.LibrarianId != librarian.LibrarianId))
                return (false, $"Username '{librarian.Username}' is already taken.");

            existing.Name = librarian.Name.Trim();
            existing.Phone = librarian.Phone.Trim();
            existing.Position = librarian.Position.Trim();
            existing.Username = librarian.Username.Trim();
            existing.Role = librarian.Role;

            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                var pwdResult = _passwordValidator.Validate(newPassword);
                if (!pwdResult.IsValid)
                    return (false, pwdResult.Errors.First().ErrorMessage);

                existing.PasswordHash = Helpers.PasswordHasher.Hash(newPassword);
            }

            _context.SaveChanges();
            return (true, "Librarian updated successfully.");
        }

        public (bool success, string message) Delete(int librarianId)
        {
            if (_context.Borrows.Any(b => b.LibrarianId == librarianId))
                return (false, "Cannot delete: this librarian has processed borrow transactions.");

            var librarian = _context.Librarians.Find(librarianId);
            if (librarian == null) return (false, "Librarian not found.");

            _context.Librarians.Remove(librarian);
            _context.SaveChanges();
            return (true, "Librarian deleted successfully.");
        }
    }
}

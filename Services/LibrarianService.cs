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
            if (!Helpers.AuthorizationHelper.CanManageLibrarians())
                return (false, "Administrator privileges are required to add a librarian account.");

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

            librarian.Name = librarian.Name.Trim();
            librarian.Gender = string.IsNullOrWhiteSpace(librarian.Gender) ? "Male" : librarian.Gender.Trim();
            librarian.Username = librarian.Username.Trim();
            librarian.Phone = librarian.Phone.Trim();
            librarian.Email = librarian.Email.Trim();
            librarian.PasswordHash = Helpers.PasswordHasher.Hash(plainPassword);
            _context.Librarians.Add(librarian);
            _context.SaveChanges();
            return (true, "Librarian added successfully.");
        }

        public (bool success, string message) Update(Librarian librarian, string? newPassword)
        {
            if (!Helpers.AuthorizationHelper.CanManageLibrarians())
                return (false, "Administrator privileges are required to update a librarian account.");

            var existing = _context.Librarians.Find(librarian.LibrarianId);
            if (existing == null) return (false, "Librarian not found.");

            var valResult = _validator.Validate(librarian);
            if (!valResult.IsValid)
                return (false, valResult.Errors.First().ErrorMessage);

            if (_context.Librarians.Any(l => l.Username.ToLower() == librarian.Username.Trim().ToLower()
                                         && l.LibrarianId != librarian.LibrarianId))
                return (false, $"Username '{librarian.Username}' is already taken.");

            existing.Name = librarian.Name.Trim();
            existing.Gender = string.IsNullOrWhiteSpace(librarian.Gender) ? "Male" : librarian.Gender.Trim();
            existing.DateOfBirth = librarian.DateOfBirth;
            existing.Phone = librarian.Phone.Trim();
            existing.Email = librarian.Email.Trim();
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
            if (!Helpers.AuthorizationHelper.CanManageLibrarians())
                return (false, "Administrator privileges are required to delete a librarian account.");

            if (librarianId == Helpers.SessionManager.CurrentLibrarian?.LibrarianId)
                return (false, "You cannot delete your own logged-in account.");

            if (_context.Borrows.Any(b => b.LibrarianId == librarianId))
                return (false, "Cannot delete: this librarian has processed borrow transactions.");

            var librarian = _context.Librarians.Find(librarianId);
            if (librarian == null) return (false, "Librarian not found.");

            try
            {
                _context.Librarians.Remove(librarian);
                _context.SaveChanges();
                return (true, "Librarian deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Cannot delete librarian: {ex.Message}");
            }
        }
    }
}

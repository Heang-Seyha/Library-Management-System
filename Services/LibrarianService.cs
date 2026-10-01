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

            // Always force new accounts to the Librarian role regardless of caller input.
            // This prevents privilege escalation from the UI layer.
            librarian.Role = Helpers.AuthorizationHelper.RoleLibrarian;

            var valResult = _validator.Validate(librarian);
            if (!valResult.IsValid)
                return (false, valResult.Errors.First().ErrorMessage);

            if (string.IsNullOrWhiteSpace(plainPassword))
                return (false, "Password is required.");

            var pwdResult = _passwordValidator.Validate(plainPassword);
            if (!pwdResult.IsValid)
                return (false, pwdResult.Errors.First().ErrorMessage);

            var trimmedUsername = librarian.Username.Trim();
            if (_context.Librarians.Any(l => l.Username == trimmedUsername))
                return (false, $"Username '{librarian.Username}' is already taken.");

            var trimmedEmail = librarian.Email.Trim();
            if (!string.IsNullOrEmpty(trimmedEmail) && _context.Librarians.Any(l => l.Email == trimmedEmail))
                return (false, $"A librarian with email '{librarian.Email}' already exists.");

            var trimmedPhone = librarian.Phone.Trim();
            if (!string.IsNullOrEmpty(trimmedPhone) && _context.Librarians.Any(l => l.Phone == trimmedPhone))
                return (false, $"A librarian with phone '{librarian.Phone}' already exists.");

            librarian.Name = librarian.Name.Trim();
            librarian.Gender = string.IsNullOrWhiteSpace(librarian.Gender) ? "Male" : librarian.Gender.Trim();
            librarian.Username = trimmedUsername;
            librarian.Phone = trimmedPhone;
            librarian.Email = trimmedEmail;
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

            // Read the original role from the database to prevent role manipulation.
            var originalRole = _context.Entry(existing).Property(e => e.Role).OriginalValue ?? existing.Role;

            // Invariant: The Admin role cannot be changed — prevents demotion of the only Admin.
            if (originalRole == Helpers.AuthorizationHelper.RoleAdmin &&
                !string.Equals(librarian.Role, Helpers.AuthorizationHelper.RoleAdmin, StringComparison.OrdinalIgnoreCase))
            {
                _context.Entry(existing).Reload();
                return (false, "Cannot change role of the Administrator account. The system must always have exactly one Admin.");
            }

            // Invariant: A Librarian cannot be promoted to Administrator.
            if (originalRole != Helpers.AuthorizationHelper.RoleAdmin &&
                string.Equals(librarian.Role, Helpers.AuthorizationHelper.RoleAdmin, StringComparison.OrdinalIgnoreCase))
            {
                _context.Entry(existing).Reload();
                return (false, "Cannot promote a Librarian to Administrator. Exactly one Administrator account is permitted.");
            }

            var valResult = _validator.Validate(librarian);
            if (!valResult.IsValid)
            {
                _context.Entry(existing).Reload();
                return (false, valResult.Errors.First().ErrorMessage);
            }

            var trimmedUsername = librarian.Username.Trim();
            if (_context.Librarians.Any(l => l.Username == trimmedUsername
                                             && l.LibrarianId != librarian.LibrarianId))
            {
                _context.Entry(existing).Reload();
                return (false, $"Username '{librarian.Username}' is already taken.");
            }

            var trimmedEmail = librarian.Email.Trim();
            if (!string.IsNullOrEmpty(trimmedEmail) && _context.Librarians.Any(l => l.Email == trimmedEmail && l.LibrarianId != librarian.LibrarianId))
            {
                _context.Entry(existing).Reload();
                return (false, $"A librarian with email '{librarian.Email}' already exists.");
            }

            var trimmedPhone = librarian.Phone.Trim();
            if (!string.IsNullOrEmpty(trimmedPhone) && _context.Librarians.Any(l => l.Phone == trimmedPhone && l.LibrarianId != librarian.LibrarianId))
            {
                _context.Entry(existing).Reload();
                return (false, $"A librarian with phone '{librarian.Phone}' already exists.");
            }

            existing.Name = librarian.Name.Trim();
            existing.Gender = string.IsNullOrWhiteSpace(librarian.Gender) ? "Male" : librarian.Gender.Trim();
            existing.DateOfBirth = librarian.DateOfBirth;
            existing.Phone = trimmedPhone;
            existing.Email = trimmedEmail;
            existing.Username = trimmedUsername;
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

            var existing = _context.Librarians.Find(librarianId);
            if (existing == null)
                return (false, "Librarian not found.");

            _context.Entry(existing).Reload();

            // The system must have exactly one Admin account — do not allow deletion of the Admin
            if (string.Equals(existing.Role, Helpers.AuthorizationHelper.RoleAdmin, StringComparison.OrdinalIgnoreCase))
                return (false, "Cannot delete the Administrator account. The system must always have exactly one Admin.");

            // Prevent deleting the currently authenticated account
            if (Helpers.SessionManager.CurrentLibrarian?.LibrarianId == librarianId)
                return (false, "Cannot delete your own account while logged in.");

            // Historical borrowing records must be preserved — reject deletion if borrowing history exists
            if (_context.Borrows.Any(b => b.LibrarianId == librarianId))
                return (false, "Cannot delete librarian because historical borrowing records are associated with this account.");

            try
            {
                _context.Librarians.Remove(existing);
                _context.SaveChanges();
                return (true, "Librarian deleted successfully.");
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return (false, "Cannot delete this librarian because they are referenced by existing library records.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[LibrarianService.Delete] {ex}");
                return (false, "An error occurred while deleting the librarian account.");
            }
        }
    }
}

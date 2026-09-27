using LibraryManagementSystem.Data;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    /// <summary>
    /// Handles librarian authentication (login/logout).
    /// 
    /// Login flow:
    ///   Username + Password → Find Librarian → Verify Hash → Return Librarian
    /// 
    /// The caller (LoginForm) then stores the result in SessionManager.
    /// </summary>
    public class AuthenticationService
    {
        private readonly LibraryDbContext _context;

        public AuthenticationService(LibraryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Attempts to authenticate a librarian by username and password.
        /// Returns the Librarian object if successful, null if credentials are invalid.
        /// </summary>
        public Librarian? Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            // Find librarian by username (case-insensitive)
            var librarian = _context.Librarians
                .FirstOrDefault(l => l.Username.ToLower() == username.Trim().ToLower());

            if (librarian == null) return null;

            // Verify password against stored BCrypt hash
            if (!PasswordHasher.Verify(password, librarian.PasswordHash))
                return null;

            return librarian;
        }
    }
}

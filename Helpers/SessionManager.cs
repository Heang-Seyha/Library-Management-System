using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Helpers
{
    /// <summary>
    /// Stores the currently logged-in librarian for the duration of the session.
    /// This is a simple, static session for a desktop WinForms application.
    /// </summary>
    public static class SessionManager
    {
        /// <summary>The currently authenticated librarian. Null if not logged in.</summary>
        public static Librarian? CurrentLibrarian { get; private set; }

        /// <summary>True if there is an authenticated librarian in the session.</summary>
        public static bool IsLoggedIn => CurrentLibrarian != null;

        /// <summary>True if the current librarian has the Admin role.</summary>
        public static bool IsAdmin => CurrentLibrarian?.Role == AuthorizationHelper.RoleAdmin;

        /// <summary>True if the current librarian has the Librarian or Admin role.</summary>
        public static bool IsLibrarian => CurrentLibrarian?.Role == AuthorizationHelper.RoleLibrarian || IsAdmin;

        /// <summary>
        /// Sets the current session after a successful login.
        /// </summary>
        public static void Login(Librarian librarian)
        {
            CurrentLibrarian = librarian ?? throw new ArgumentNullException(nameof(librarian));
        }

        /// <summary>
        /// Clears the session — called on logout.
        /// </summary>
        public static void Clear()
        {
            CurrentLibrarian = null;
        }

        /// <summary>
        /// Alias for Clear() to end the authenticated session.
        /// </summary>
        public static void Logout() => Clear();
    }
}

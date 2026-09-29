using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Helpers
{
    /// <summary>
    /// Centralized authorization helper and policy enforcement for the Library Management System.
    /// Provides consistent role definitions and permission evaluations across both UI and Service layers.
    /// </summary>
    public static class AuthorizationHelper
    {
        public const string RoleAdmin = "Admin";
        public const string RoleLibrarian = "Librarian";

        /// <summary>
        /// Only Admins can manage librarian accounts (Add, Edit, Delete).
        /// </summary>
        public static bool CanManageLibrarians(string? role = null) =>
            role != null ? role == RoleAdmin : SessionManager.IsAdmin;

        /// <summary>
        /// Both Admin and Librarian can manage books.
        /// </summary>
        public static bool CanManageBooks(string? role = null) =>
            role != null ? (role == RoleAdmin || role == RoleLibrarian) : SessionManager.IsLoggedIn;

        /// <summary>
        /// Both Admin and Librarian can manage members.
        /// </summary>
        public static bool CanManageMembers(string? role = null) =>
            role != null ? (role == RoleAdmin || role == RoleLibrarian) : SessionManager.IsLoggedIn;

        /// <summary>
        /// Both Admin and Librarian can manage categories.
        /// </summary>
        public static bool CanManageCategories(string? role = null) =>
            role != null ? (role == RoleAdmin || role == RoleLibrarian) : SessionManager.IsLoggedIn;

        /// <summary>
        /// Both Admin and Librarian can manage authors.
        /// </summary>
        public static bool CanManageAuthors(string? role = null) =>
            role != null ? (role == RoleAdmin || role == RoleLibrarian) : SessionManager.IsLoggedIn;

        /// <summary>
        /// Both Admin and Librarian can manage publishers.
        /// </summary>
        public static bool CanManagePublishers(string? role = null) =>
            role != null ? (role == RoleAdmin || role == RoleLibrarian) : SessionManager.IsLoggedIn;

        /// <summary>
        /// Both Admin and Librarian can manage master data (Categories, Authors, Publishers).
        /// </summary>
        public static bool CanManageMetadata(string? role = null) =>
            role != null ? (role == RoleAdmin || role == RoleLibrarian) : SessionManager.IsLoggedIn;

        /// <summary>
        /// Both Admin and Librarian can perform borrow and return operations.
        /// </summary>
        public static bool CanCirculate(string? role = null) =>
            role != null ? (role == RoleAdmin || role == RoleLibrarian) : SessionManager.IsLoggedIn;

        /// <summary>
        /// Both Admin and Librarian can view reports.
        /// </summary>
        public static bool CanViewReports(string? role = null) =>
            role != null ? (role == RoleAdmin || role == RoleLibrarian) : SessionManager.IsLoggedIn;

        /// <summary>
        /// Both Admin and Librarian can export reports.
        /// </summary>
        public static bool CanExportReports(string? role = null) =>
            role != null ? (role == RoleAdmin || role == RoleLibrarian) : SessionManager.IsLoggedIn;

        /// <summary>
        /// Both Admin and Librarian can view and export reports.
        /// </summary>
        public static bool CanViewAndExportReports(string? role = null) =>
            role != null ? (role == RoleAdmin || role == RoleLibrarian) : SessionManager.IsLoggedIn;

        /// <summary>
        /// Throws an UnauthorizedAccessException if the current session is not Admin.
        /// Used to guard sensitive service-layer operations.
        /// </summary>
        public static void RequireAdmin()
        {
            if (!SessionManager.IsAdmin)
            {
                throw new UnauthorizedAccessException("Administrator privileges are required to perform this action.");
            }
        }

        /// <summary>
        /// Throws an UnauthorizedAccessException if no user is currently authenticated.
        /// </summary>
        public static void RequireAuthenticated()
        {
            if (!SessionManager.IsLoggedIn)
            {
                throw new UnauthorizedAccessException("Authentication is required to perform this action.");
            }
        }
    }
}

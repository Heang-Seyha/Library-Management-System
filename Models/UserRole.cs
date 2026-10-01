namespace LibraryManagementSystem.Models
{
    /// <summary>
    /// User roles within the Library Management System.
    /// Exactly one Admin account exists; all other staff members are Librarians.
    /// Stored as string in the database for backwards compatibility.
    /// </summary>
    public enum UserRole
    {
        Admin,
        Librarian
    }
}

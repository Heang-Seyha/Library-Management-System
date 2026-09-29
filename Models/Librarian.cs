namespace LibraryManagementSystem.Models
{
    /// <summary>
    /// Represents a library staff member (Librarian).
    /// Demonstrates: Inheritance (extends Person), Polymorphism (overrides GetInfo)
    /// </summary>
    public class Librarian : Person
    {
        public int LibrarianId { get; set; }
        public string Email { get; set; } = string.Empty;

        // Authentication fields — never store plain text password
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Role: "Admin" or "Librarian"
        public string Role { get; set; } = "Librarian";

        // Navigation property — Association: Librarian processes many Borrows
        public ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();

        // Demonstrates Polymorphism — overrides Person.GetInfo()
        public override string GetInfo()
        {
            string dobStr = DateOfBirth.HasValue ? DateOfBirth.Value.ToString("MM/dd/yyyy") : "N/A";
            return $"Librarian: {Name} | Gender: {Gender} | DOB: {dobStr} | Phone: {Phone} | Email: {Email} | Role: {Role}";
        }

        public bool IsAdmin => Role == "Admin";
        public bool IsLibrarian => Role == "Librarian" || IsAdmin;

        public override string ToString() => Name;

        // Backward compatibility property for database mapping
        public int EmployeeId
        {
            get => LibrarianId;
            set => LibrarianId = value;
        }
    }
}

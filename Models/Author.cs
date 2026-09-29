namespace LibraryManagementSystem.Models
{
    /// <summary>
    /// Represents a book author.
    /// Demonstrates: Inheritance (extends Person), Polymorphism (overrides GetInfo)
    /// </summary>
    public class Author : Person
    {
        public int AuthorId { get; set; }
        public string Bio { get; set; } = string.Empty;

        // Navigation property — one Author has many Books
        public ICollection<Book> Books { get; set; } = new List<Book>();

        // Demonstrates Polymorphism — overrides Person.GetInfo()
        public override string GetInfo()
        {
            string dobStr = DateOfBirth.HasValue ? DateOfBirth.Value.ToString("MM/dd/yyyy") : "N/A";
            return $"Author: {Name} | Gender: {Gender} | DOB: {dobStr} | Bio: {Bio}";
        }

        public override string ToString() => Name;
    }
}

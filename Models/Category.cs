namespace LibraryManagementSystem.Models
{
    /// <summary>
    /// Represents a book category/genre.
    /// </summary>
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation property — one Category has many Books
        public ICollection<Book> Books { get; set; } = new List<Book>();

        public override string ToString() => Name;
    }
}

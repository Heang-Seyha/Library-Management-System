namespace LibraryManagementSystem.Models
{
    /// <summary>
    /// Represents a book author.
    /// </summary>
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;

        // Navigation property — one Author has many Books
        public ICollection<Book> Books { get; set; } = new List<Book>();

        public override string ToString() => Name;
    }
}

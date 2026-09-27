namespace LibraryManagementSystem.Models
{
    /// <summary>
    /// Represents a book publisher.
    /// </summary>
    public class Publisher
    {
        public int PublisherId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        // Navigation property — one Publisher has many Books
        public ICollection<Book> Books { get; set; } = new List<Book>();

        public override string ToString() => Name;
    }
}

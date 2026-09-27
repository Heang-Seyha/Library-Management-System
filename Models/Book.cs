namespace LibraryManagementSystem.Models
{
    /// <summary>
    /// Represents a physical book in the library.
    /// Demonstrates: Association with Category, Author, Publisher, BorrowDetail
    /// </summary>
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int Year { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        // Optimistic concurrency token to prevent race conditions on inventory updates
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        // Foreign Keys
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }

        // Navigation properties — Association
        public Category? Category { get; set; }
        public Author? Author { get; set; }
        public Publisher? Publisher { get; set; }

        // One book can appear in many borrow detail records
        public ICollection<BorrowDetail> BorrowDetails { get; set; } = new List<BorrowDetail>();

        public override string ToString() => Title;
    }
}

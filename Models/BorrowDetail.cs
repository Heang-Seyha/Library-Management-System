namespace LibraryManagementSystem.Models
{
    /// <summary>
    /// Represents a single line item within a Borrow transaction.
    /// One BorrowDetail links one Borrow to one Book with a quantity.
    /// This allows multiple books in a single borrow transaction.
    /// Demonstrates: Association (Borrow, Book)
    /// </summary>
    public class BorrowDetail
    {
        public int BorrowDetailId { get; set; }

        // Foreign Keys
        public int BorrowId { get; set; }
        public int BookId { get; set; }

        // How many copies of this book were borrowed
        public int Quantity { get; set; }

        // Navigation properties — Association
        public Borrow? Borrow { get; set; }
        public Book? Book { get; set; }
    }
}

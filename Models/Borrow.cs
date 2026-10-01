namespace LibraryManagementSystem.Models
{
    /// <summary>
    /// Fine rate configuration — single source of truth.
    /// Change this value to adjust the fine system-wide.
    /// </summary>
    public static class FinePolicy
    {
        /// <summary>Fine amount per overdue day in Cambodian Riel.</summary>
        public const decimal FinePerDay = 2000m;

        /// <summary>
        /// Calculates the fine for a given borrow transaction.
        /// Fine = max(0, OverdueDays) × FinePerDay
        /// </summary>
        public static decimal CalculateFine(DateTime dueDate, DateTime returnDate)
        {
            var overdueDays = (returnDate.Date - dueDate.Date).Days;
            if (overdueDays <= 0) return 0m;
            return overdueDays * FinePerDay;
        }
    }

    /// <summary>
    /// Represents the status of a borrow transaction.
    /// </summary>
    public static class BorrowStatus
    {
        public const string Active = "Active";
        public const string Returned = "Returned";
        public const string Overdue = "Overdue";
    }

    /// <summary>
    /// Represents a single borrow transaction made by a Member, processed by a Librarian.
    /// One Borrow contains one or more BorrowDetails (one per book).
    /// Demonstrates: Association (Member, Librarian, BorrowDetail)
    /// </summary>
    public class Borrow
    {
        public int BorrowId { get; set; }

        // Foreign Keys
        public int MemberId { get; set; }
        public int LibrarianId { get; set; }

        public DateTime BorrowDate { get; set; } = DateTime.Today;
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // Fine amount in Cambodian Riel (0 if returned on time)
        public decimal FineAmount { get; set; } = 0m;

        // "Active", "Returned", "Overdue"
        public string Status { get; set; } = BorrowStatus.Active;

        // Navigation properties — Association
        public Member? Member { get; set; }
        public Librarian? Librarian { get; set; }

        // Concurrency token — prevents double-returns and concurrent race conditions
        public byte[]? RowVersion { get; set; }

        // One Borrow has many BorrowDetails (one per book type borrowed)
        public ICollection<BorrowDetail> BorrowDetails { get; set; } = new List<BorrowDetail>();
    }
}

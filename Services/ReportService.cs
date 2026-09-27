using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    /// <summary>
    /// Provides data for all reports.
    /// Each method returns data ready to display in a DataGridView.
    /// </summary>
    public class ReportService
    {
        private readonly LibraryDbContext _context;

        public ReportService(LibraryDbContext context)
        {
            _context = context;
        }

        /// <summary>All currently active (not returned) borrows.</summary>
        public List<BorrowReportRow> GetActiveBorrows()
        {
            return _context.Borrows
                .Include(b => b.Member)
                .Include(b => b.Librarian)
                .Include(b => b.BorrowDetails).ThenInclude(bd => bd.Book)
                .Where(b => b.Status == BorrowStatus.Active || b.Status == BorrowStatus.Overdue)
                .OrderByDescending(b => b.BorrowDate)
                .AsEnumerable()
                .Select(b => new BorrowReportRow
                {
                    BorrowId = b.BorrowId,
                    MemberName = b.Member?.Name ?? "",
                    LibrarianName = b.Librarian?.Name ?? "",
                    Books = string.Join(", ", b.BorrowDetails.Select(bd => $"{bd.Book?.Title} ×{bd.Quantity}")),
                    BorrowDate = b.BorrowDate,
                    DueDate = b.DueDate,
                    Status = b.Status,
                    DaysOverdue = b.DueDate.Date < DateTime.Today ? (DateTime.Today - b.DueDate.Date).Days : 0,
                    EstimatedFine = b.DueDate.Date < DateTime.Today
                        ? FinePolicy.CalculateFine(b.DueDate, DateTime.Today) : 0m
                })
                .ToList();
        }

        /// <summary>All returned borrows with fine amounts.</summary>
        public List<BorrowReportRow> GetReturnedBorrows()
        {
            return _context.Borrows
                .Include(b => b.Member)
                .Include(b => b.Librarian)
                .Include(b => b.BorrowDetails).ThenInclude(bd => bd.Book)
                .Where(b => b.Status == BorrowStatus.Returned)
                .OrderByDescending(b => b.ReturnDate)
                .AsEnumerable()
                .Select(b => new BorrowReportRow
                {
                    BorrowId = b.BorrowId,
                    MemberName = b.Member?.Name ?? "",
                    LibrarianName = b.Librarian?.Name ?? "",
                    Books = string.Join(", ", b.BorrowDetails.Select(bd => $"{bd.Book?.Title} ×{bd.Quantity}")),
                    BorrowDate = b.BorrowDate,
                    DueDate = b.DueDate,
                    ReturnDate = b.ReturnDate,
                    Status = b.Status,
                    Fine = b.FineAmount
                })
                .ToList();
        }

        /// <summary>Only overdue borrows.</summary>
        public List<BorrowReportRow> GetOverdueBorrows()
        {
            return GetActiveBorrows().Where(r => r.DaysOverdue > 0).ToList();
        }

        /// <summary>Total fines collected (from returned borrows with fine > 0).</summary>
        public List<FineReportRow> GetFineReport()
        {
            return _context.Borrows
                .Include(b => b.Member)
                .Where(b => b.Status == BorrowStatus.Returned && b.FineAmount > 0)
                .OrderByDescending(b => b.ReturnDate)
                .AsEnumerable()
                .Select(b => new FineReportRow
                {
                    BorrowId = b.BorrowId,
                    MemberName = b.Member?.Name ?? "",
                    DueDate = b.DueDate,
                    ReturnDate = b.ReturnDate ?? DateTime.MinValue,
                    DaysOverdue = (int)(b.ReturnDate!.Value.Date - b.DueDate.Date).TotalDays,
                    FineAmount = b.FineAmount
                })
                .ToList();
        }

        /// <summary>Top 10 most borrowed books.</summary>
        public List<PopularBookRow> GetMostBorrowedBooks()
        {
            return _context.BorrowDetails
                .Include(bd => bd.Book).ThenInclude(b => b!.Author)
                .GroupBy(bd => bd.BookId)
                .Select(g => new PopularBookRow
                {
                    Title = g.First().Book!.Title,
                    Author = g.First().Book!.Author!.Name,
                    TotalBorrowed = g.Sum(bd => bd.Quantity)
                })
                .OrderByDescending(r => r.TotalBorrowed)
                .Take(10)
                .ToList();
        }

        /// <summary>Top 10 most active members.</summary>
        public List<ActiveMemberRow> GetMostActiveMembers()
        {
            return _context.Borrows
                .Include(b => b.Member)
                .GroupBy(b => b.MemberId)
                .Select(g => new ActiveMemberRow
                {
                    MemberName = g.First().Member!.Name,
                    TotalBorrows = g.Count(),
                    TotalFines = g.Sum(b => b.FineAmount)
                })
                .OrderByDescending(r => r.TotalBorrows)
                .Take(10)
                .ToList();
        }

        /// <summary>Current book inventory.</summary>
        public List<InventoryRow> GetInventory()
        {
            return _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .OrderBy(b => b.Title)
                .Select(b => new InventoryRow
                {
                    Title = b.Title,
                    ISBN = b.ISBN,
                    Author = b.Author!.Name,
                    Category = b.Category!.Name,
                    TotalCopies = b.TotalCopies,
                    AvailableCopies = b.AvailableCopies,
                    BorrowedCopies = b.TotalCopies - b.AvailableCopies
                })
                .ToList();
        }

        // ── Summary Totals ────────────────────────────────────────────────────

        public decimal GetTotalFinesCollected() =>
            _context.Borrows.Where(b => b.Status == BorrowStatus.Returned).Sum(b => b.FineAmount);

        public int GetTotalBorrowCount() => _context.Borrows.Count();

        public int GetOverdueCount() => _context.Borrows.Count(b => b.Status == BorrowStatus.Overdue);
    }

    // ── Report DTOs ───────────────────────────────────────────────────────────

    public class BorrowReportRow
    {
        public int BorrowId { get; set; }
        public string MemberName { get; set; } = "";
        public string LibrarianName { get; set; } = "";
        public string EmployeeName
        {
            get => LibrarianName;
            set => LibrarianName = value;
        }
        public string Books { get; set; } = "";
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = "";
        public int DaysOverdue { get; set; }
        public decimal EstimatedFine { get; set; }
        public decimal Fine { get; set; }
    }

    public class FineReportRow
    {
        public int BorrowId { get; set; }
        public string MemberName { get; set; } = "";
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int DaysOverdue { get; set; }
        public decimal FineAmount { get; set; }
    }

    public class PopularBookRow
    {
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public int TotalBorrowed { get; set; }
    }

    public class ActiveMemberRow
    {
        public string MemberName { get; set; } = "";
        public int TotalBorrows { get; set; }
        public decimal TotalFines { get; set; }
    }

    public class InventoryRow
    {
        public string Title { get; set; } = "";
        public string ISBN { get; set; } = "";
        public string Author { get; set; } = "";
        public string Category { get; set; } = "";
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public int BorrowedCopies { get; set; }
    }
}

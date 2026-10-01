using FluentValidation;

namespace LibraryManagementSystem.Validators
{
    /// <summary>
    /// Represents the input data for a borrow transaction request.
    /// </summary>
    public class BorrowRequest
    {
        public int MemberId { get; set; }
        public int LibrarianId { get; set; }
        public DateTime DueDate { get; set; }
        public List<(int bookId, int quantity)> Items { get; set; } = new();

        public BorrowRequest() { }

        public BorrowRequest(int memberId, int librarianId, DateTime dueDate, List<(int bookId, int quantity)> items)
        {
            MemberId = memberId;
            LibrarianId = librarianId;
            DueDate = dueDate;
            Items = items;
        }
    }

    /// <summary>
    /// Validates the borrow intent / request shape before database queries:
    /// - Member and Librarian IDs must be valid positive integers
    /// - Due date must not be in the past
    /// - At least one book must be selected
    /// - Quantities must be at least 1
    /// Live inventory and DB existence checks remain in BorrowService.
    /// </summary>
    public class BorrowRequestValidator : AbstractValidator<BorrowRequest>
    {
        public BorrowRequestValidator()
        {
            RuleFor(r => r.MemberId)
                .GreaterThan(0)
                .WithMessage("Member ID must be valid.");

            RuleFor(r => r.LibrarianId)
                .GreaterThan(0)
                .WithMessage("Librarian ID must be valid.");

            RuleFor(r => r.DueDate.Date)
                .GreaterThan(DateTime.Today)
                .WithMessage("Due date must be at least one day after borrow date.");

            RuleFor(r => r.Items)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Please select at least one book.")
                .Must(items => items != null && items.Count > 0).WithMessage("Please select at least one book.");

            RuleForEach(r => r.Items)
                .Must(item => item.bookId > 0)
                .WithMessage("Invalid book ID.");

            RuleForEach(r => r.Items)
                .Must(item => item.quantity >= 1)
                .WithMessage("Quantity must be at least 1.");
        }
    }
}

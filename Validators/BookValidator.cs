using FluentValidation;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Validators
{
    /// <summary>
    /// Validator for Book entity shape and format rules.
    /// Pure and stateless; database uniqueness is checked at the service layer.
    /// </summary>
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Book title is required.")
                .MaximumLength(300).WithMessage("Book title cannot exceed 300 characters.");

            RuleFor(b => b.ISBN)
                .NotEmpty().WithMessage("ISBN is required.")
                .Must(ValidationHelper.IsValidISBN)
                .WithMessage("ISBN must be a valid ISBN-10 or ISBN-13.");

            RuleFor(b => b.TotalCopies)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Total copies must be at least 1.");

            RuleFor(b => b.AvailableCopies)
                .InclusiveBetween(0, int.MaxValue)
                .WithMessage("Available copies must be between 0 and total copies.")
                .Must((book, available) => available <= book.TotalCopies)
                .WithMessage("Available copies must be between 0 and total copies.");

            RuleFor(b => b.Year)
                .Must(ValidationHelper.IsValidYear)
                .WithMessage("Publication year is not valid.");

            RuleFor(b => b.CategoryId)
                .GreaterThan(0)
                .WithMessage("Please select a valid category.");

            RuleFor(b => b.AuthorId)
                .GreaterThan(0)
                .WithMessage("Please select a valid author.");

            RuleFor(b => b.PublisherId)
                .GreaterThan(0)
                .WithMessage("Please select a valid publisher.");
        }
    }
}

using FluentValidation;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Validators
{
    /// <summary>
    /// Validator for Author entity shape and format rules.
    /// Pure and stateless.
    /// </summary>
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Author name is required.")
                .MaximumLength(150).WithMessage("Author name cannot exceed 150 characters.");

            RuleFor(a => a.Bio)
                .MaximumLength(1000).WithMessage("Bio cannot exceed 1000 characters.");
        }
    }
}

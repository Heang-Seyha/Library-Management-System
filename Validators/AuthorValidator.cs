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

            RuleFor(a => a.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .Must(g => g == "Male" || g == "Female").WithMessage("Gender must be 'Male' or 'Female'.");

            RuleFor(a => a.DateOfBirth)
                .NotNull().WithMessage("Date of birth is required.")
                .Must(dob => !dob.HasValue || dob.Value.Date <= DateTime.Today)
                .WithMessage("Date of birth cannot be in the future.");

            RuleFor(a => a.Bio)
                .MaximumLength(1000).WithMessage("Bio cannot exceed 1000 characters.");
        }
    }
}

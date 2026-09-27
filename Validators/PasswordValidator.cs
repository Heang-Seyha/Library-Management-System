using FluentValidation;
using LibraryManagementSystem.Helpers;

namespace LibraryManagementSystem.Validators
{
    /// <summary>
    /// Validator for user password strength.
    /// Enforces: Minimum 8 characters, at least one letter, and at least one digit.
    /// </summary>
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
        {
            RuleFor(p => p)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Must(ValidationHelper.IsValidPassword)
                .WithMessage("Password must be at least 8 characters with at least one letter and one digit.");
        }
    }
}

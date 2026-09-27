using FluentValidation;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Validators
{
    /// <summary>
    /// Validator for Librarian entity shape and format rules.
    /// Strictly validates Name, Username, Role ('Admin' or 'Librarian'), Position, and Phone.
    /// </summary>
    public class LibrarianValidator : AbstractValidator<Librarian>
    {
        private readonly PasswordValidator _passwordValidator = new();

        public LibrarianValidator()
        {
            RuleFor(l => l.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(150).WithMessage("Name cannot exceed 150 characters.");

            RuleFor(l => l.Username)
                .NotEmpty().WithMessage("Username is required.")
                .Must(u => ValidationHelper.IsValidUsername(u, 3, 50))
                .WithMessage("Username must be 3-50 alphanumeric characters or underscores.");

            RuleFor(l => l.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(r => r == "Admin" || r == "Librarian")
                .WithMessage("Role must be 'Admin' or 'Librarian'.");

            RuleFor(l => l.Position)
                .MaximumLength(100).WithMessage("Position cannot exceed 100 characters.");

            RuleFor(l => l.Phone)
                .MaximumLength(20).WithMessage("Phone cannot exceed 20 characters.")
                .Must(ValidationHelper.IsValidPhone)
                .When(l => !string.IsNullOrWhiteSpace(l.Phone))
                .WithMessage("Phone number is not valid.");
        }

        /// <summary>
        /// Validates password strength using the dedicated PasswordValidator.
        /// </summary>
        public FluentValidation.Results.ValidationResult ValidatePassword(string password)
        {
            return _passwordValidator.Validate(password);
        }
    }
}

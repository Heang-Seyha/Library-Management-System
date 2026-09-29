using FluentValidation;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Validators
{
    /// <summary>
    /// Validator for Librarian entity shape and format rules.
    /// Strictly validates Name, Username, Role ('Admin' or 'Librarian'), Phone, and Email.
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

            RuleFor(l => l.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .Must(g => g == "Male" || g == "Female").WithMessage("Gender must be 'Male' or 'Female'.");

            RuleFor(l => l.DateOfBirth)
                .NotNull().WithMessage("Date of birth is required.")
                .Must(dob => !dob.HasValue || dob.Value.Date <= DateTime.Today)
                .WithMessage("Date of birth cannot be in the future.");

            RuleFor(l => l.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(r => r == "Admin" || r == "Librarian")
                .WithMessage("Role must be 'Admin' or 'Librarian'.");

            RuleFor(l => l.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Must(ValidationHelper.IsValidPhone).WithMessage("Phone number is not valid.")
                .MaximumLength(20).WithMessage("Phone cannot exceed 20 characters.");

            RuleFor(l => l.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .Must(ValidationHelper.IsValidEmail).WithMessage("Email address is not valid.")
                .MaximumLength(200).WithMessage("Email cannot exceed 200 characters.");
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

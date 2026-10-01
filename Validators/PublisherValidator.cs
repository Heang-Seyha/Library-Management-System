using FluentValidation;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Validators
{
    /// <summary>
    /// Validator for Publisher entity shape and format rules.
    /// Pure and stateless.
    /// </summary>
    public class PublisherValidator : AbstractValidator<Publisher>
    {
        public PublisherValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Publisher name is required.")
                .MaximumLength(150).WithMessage("Publisher name cannot exceed 150 characters.");

            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.");

            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .MaximumLength(20).WithMessage("Phone cannot exceed 20 characters.")
                .Must(ValidationHelper.IsValidPhone)
                .WithMessage("Invalid phone (must start with 0, 9-10 digits).");
        }
    }
}

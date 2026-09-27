using FluentValidation;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Validators
{
    /// <summary>
    /// Validator for Member entity shape and format rules.
    /// Pure and stateless.
    /// </summary>
    public class MemberValidator : AbstractValidator<Member>
    {
        public MemberValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Member name is required.")
                .MaximumLength(150).WithMessage("Name cannot exceed 150 characters.");

            RuleFor(m => m.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Must(ValidationHelper.IsValidPhone).WithMessage("Phone number is not valid.")
                .MaximumLength(20).WithMessage("Phone cannot exceed 20 characters.");

            RuleFor(m => m.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .Must(ValidationHelper.IsValidEmail).WithMessage("Email address is not valid.")
                .MaximumLength(200).WithMessage("Email cannot exceed 200 characters.");

            RuleFor(m => m.Address)
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.");
        }
    }
}

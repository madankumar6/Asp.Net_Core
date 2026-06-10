using ECommerce.UserService.Core.Common.Enums;
using ECommerce.UserService.Core.Dtos;
using FluentValidation;

namespace ECommerce.UserService.Core.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email is not provided in the email format");

            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Password is required");

            RuleFor(x => x.PersonName).NotEmpty()
                .WithMessage("Name is required")
                .MinimumLength(1)
                .WithMessage("Minimum 1 character is required")
                .MaximumLength(50)
                .WithMessage("Maximum 50 characters allowed");

            RuleFor(x => x.Gender).NotEmpty()
                .WithMessage("Gender is required")
                .IsInEnum()
                .WithMessage("Gender is male, female or others");
        }
    }
}

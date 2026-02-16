using ECommerce.UserService.Core.Dtos;
using FluentValidation;

namespace ECommerce.UserService.Core.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email is not provided in the email format");
            
            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Password is required");
        }
    }
}

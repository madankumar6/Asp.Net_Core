using ECommerce.ProductsService.Core.Dtos.Request;
using FluentValidation;

namespace ECommerce.ProductsService.Core.Validators
{
    public class ProductAddRequestValidator : AbstractValidator<ProductAddRequest>
    {
        public ProductAddRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Product name is required.")
                .MaximumLength(100)
                .WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage("Product category is required.")
                .MaximumLength(50)
                .WithMessage("Product category must not exceed 50 characters.");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Product price is required.")
                .GreaterThan(0)
                .WithMessage("Product price must be greater than zero.");

            RuleFor(x => x.QuantityInStock)
                .NotEmpty()
                .WithMessage("Quantity in stock is required.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity in stock cannot be negative.");
        }
    }
}

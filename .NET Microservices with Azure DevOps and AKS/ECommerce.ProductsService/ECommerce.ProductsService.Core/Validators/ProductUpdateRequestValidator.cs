using ECommerce.ProductsService.Core.Dtos.Request;
using FluentValidation;

namespace ECommerce.ProductsService.Core.Validators
{
    public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
    {
        public ProductUpdateRequestValidator()
        {
            RuleFor(x => x.ProductID)
                .NotEmpty()
                .WithMessage("Product ID is required.");
            RuleFor(x => x.ProductName)
                .NotEmpty()
                .WithMessage("Product name is required.")
                .MaximumLength(100)
                .WithMessage("Product name must not exceed 100 characters.");
            RuleFor(x => x.Category)
                .IsInEnum()
                .WithMessage("Product category is required.");
            RuleFor(x => x.UnitPrice)
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

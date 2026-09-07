
namespace Order.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

    public record UpdateOrderResult(bool IsSuccessful);

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Order.Id).NotEmpty().WithMessage("OrderId cannot be empty");
            RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId cannot be empty");
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("OrderName cannot be empty");
            RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems cannot be empty");
            RuleForEach(x => x.Order.OrderItems).SetValidator(new OrderItemValidator());
        }
    }

    public class OrderItemValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId cannot be empty");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be a positive value");
        }
    }
}

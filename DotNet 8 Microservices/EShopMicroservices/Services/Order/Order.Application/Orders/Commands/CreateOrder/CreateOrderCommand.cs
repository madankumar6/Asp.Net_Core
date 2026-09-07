
namespace Order.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

    public record CreateOrderResult(Guid OrderId);

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
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

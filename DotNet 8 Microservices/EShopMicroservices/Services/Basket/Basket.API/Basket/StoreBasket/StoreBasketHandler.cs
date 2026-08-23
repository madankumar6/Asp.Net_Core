
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.ShoppingCart).NotNull().WithMessage("Shopping cart cannot be null.");
            RuleFor(x => x.ShoppingCart.UserName).NotEmpty().When(x => x.ShoppingCart != null).WithMessage("User name cannot be empty.");
            RuleFor(x => x.ShoppingCart.Items).NotEmpty().When(x => x.ShoppingCart != null).WithMessage("Shopping cart items cannot be empty.");
        }
    }

    internal class StoreBasketCommandHandler(IBasketRepository repository, 
        DiscountProtoService.DiscountProtoServiceClient discountProtoService) 
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            foreach (var item in command.ShoppingCart.Items)
            {
                var coupon = discountProtoService.GetDiscount(new GetDiscountRequest() { ProductName = item.ProductName });
                item.Price -= coupon.Amount;
            }

            var basket = await repository.StoreBasket(command.ShoppingCart, cancellationToken);
            return new StoreBasketResult(basket.UserName);
        }
    }
}

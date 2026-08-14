
namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : ICommad<GetBasketResult>;
    public record GetBasketResult(ShoppingCart ShoppingCart);

    internal class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(query.UserName);
            return new GetBasketResult(basket);
        }
    }
}

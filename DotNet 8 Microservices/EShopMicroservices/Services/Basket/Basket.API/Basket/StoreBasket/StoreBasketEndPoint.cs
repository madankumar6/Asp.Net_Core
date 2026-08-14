
namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart ShoppingCart);
    public record StoreBasketResponse(string UserName);

    public class StoreBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();
                var result = await sender.Send(command, CancellationToken.None);
                return Results.Created($"/basket/{result.UserName}", result.Adapt<StoreBasketResponse>());
            })
            .WithName("StoreBasket")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Store a basket for a user")
            .WithDescription("Store a basket for a user");
        }
    }
}

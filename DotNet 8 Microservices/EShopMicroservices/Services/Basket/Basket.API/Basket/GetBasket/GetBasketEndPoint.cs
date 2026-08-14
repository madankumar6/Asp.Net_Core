
namespace Basket.API.Basket.GetBasket
{
    public record GetBasketRequest(string UserName);
    public record GetBasketResponse(ShoppingCart ShoppingCart);

    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var query = new GetBasketQuery(userName);
                var result = await sender.Send(query, CancellationToken.None);
                return Results.Ok(result.Adapt<GetBasketResponse>());
            })
            .WithName("GetBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Get a basket by user name")
            .WithDescription("Get a basket by user name");
        }
    }
}

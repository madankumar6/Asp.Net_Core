
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketRequest(string UserName);
    public record DeleteBasketResponse(bool IsSuccess);

    public class DeleteBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var command = new DeleteBasketCommand(userName);
                var result = await sender.Send(command, CancellationToken.None);
                return Results.Ok(result.Adapt<DeleteBasketResponse>());
            })
            .WithName("DeleteBasket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Delete a basket by user name")
            .WithDescription("Delete a basket by user name");

        }
    }
}

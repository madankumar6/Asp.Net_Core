using Order.Application.Orders.Queries.GetOrdersByName;

namespace Order.API.EndPoints
{
    // This class is a placeholder for the GetOrdersByName endpoint.
    // Accepts a GetOrdersByNameRequest and returns a GetOrdersByNameResponse.
    // Maps the request to a GetOrdersByNameQuery and sends it to the mediator.
    // Returns a response with the list of orders.

    public record GetOrdersByNameRequest(string OrderName);
    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
            {
                var query = new GetOrdersByNameQuery(orderName);
                var result = await sender.Send(query);
                return Results.Ok(new GetOrdersByNameResponse(result.Orders));
            })
            .WithName("GetOrdersByName")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders By Name")
            .WithDescription("Get Orders By Name");
        }
    }
}

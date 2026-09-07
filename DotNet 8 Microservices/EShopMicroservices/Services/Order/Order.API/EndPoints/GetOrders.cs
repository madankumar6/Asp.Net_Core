using BuildingBlocks.Pagination;
using Order.Application.Orders.Queries.GetOrders;

namespace Order.API.EndPoints
{
    // This class is a placeholder for the GetOrders endpoint.
    // Accepts a GetOrdersRequest and returns a GetOrdersResponse.
    // Maps the request to a GetOrdersQuery and sends it to the mediator.
    // Returns a response with the list of orders.
    public record GetOrdersRequest(PaginationRequest Pagination);
    public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var query = new GetOrdersQuery(request);
                var result = await sender.Send(query);
                return Results.Ok(new GetOrdersResponse(result.Orders));
            })
            .WithName("GetOrders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders")
            .WithDescription("Get Orders");
        }
    }
}

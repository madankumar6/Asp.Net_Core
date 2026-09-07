using Order.Application.Orders.Queries.GetOrdersByCustomer;
using Order.Application.Orders.Queries.GetOrdersByName;

namespace Order.API.EndPoints
{
    // This class is a placeholder for the GetOrdersByCustomer endpoint.
    // Accepts a GetOrdersByCustomerRequest and returns a GetOrdersByCustomerResponse.
    // Maps the request to a GetOrdersByCustomerQuery and sends it to the mediator.
    // Returns a response with the list of orders.
    public record GetOrdersByCustomerRequest(Guid CustomerId);
    public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var query = new GetOrdersByCustomerQuery(customerId);
                var result = await sender.Send(query);
                return Results.Ok(new GetOrdersByCustomerResponse(result.Orders));
            })
            .WithName("GetOrdersByCustomer")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders By Customer")
            .WithDescription("Get Orders By Customer");
        }
    }
}

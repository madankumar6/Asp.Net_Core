using Order.Application.Orders.Commands.UpdateOrder;

namespace Order.API.EndPoints
{
    // This class is a placeholder for the UpdateOrder endpoint.
    // Accepts an UpdateOrderRequest and returns an UpdateOrderResponse.
    // Maps the request to an UpdateOrderCommand and sends it to the mediator.
    // Returns a response with the updated order's ID and a success message.
    public record UpdateOrderRequest(OrderDto Order);
    public record UpdateOrderResponse(bool IsSuccessful);

    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (UpdateOrderRequest request, IMediator mediator) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();
                var orderResult = await mediator.Send(command);
                var response = orderResult.Adapt<UpdateOrderResponse>();
                return Results.Ok(response);
            })
            .WithName("UpdateOrder")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Order")
            .WithDescription("Update Order");
        }
    }
}

using Order.Application.Orders.Commands.DeleteOrder;
using Order.Application.Orders.Commands.UpdateOrder;

namespace Order.API.EndPoints
{
    // This class is a placeholder for the DeleteOrder endpoint.
    // Accepts a DeleteOrderRequest and returns a DeleteOrderResponse.
    // Maps the request to a DeleteOrderCommand and sends it to the mediator.
    // Returns a response with the deleted order's ID and a success message.
    /// <summary>
    /// Represents a request to delete an order.
    /// </summary>
    //public record DeleteOrderRequest(Guid OrderId);
    public record DeleteOrderResponse(bool IsSuccessful);

    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{id}", async (Guid id, IMediator mediator) =>
            {
                var command = new DeleteOrderCommand(id);
                var orderResult = await mediator.Send(command);
                var response = orderResult.Adapt<DeleteOrderResponse>();
                return Results.Ok(response);
            })
            .WithName("DeleteOrder")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Order")
            .WithDescription("Delete Order");
        }
    }
}

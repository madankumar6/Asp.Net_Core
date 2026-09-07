using Order.Application.Orders.Commands.CreateOrder;

namespace Order.API.EndPoints
{
    // This class is a placeholder for the CreateOrder endpoint.
    // Accepts a CreateOrderRequest and returns a CreateOrderResponse.
    // Maps the request to a CreateOrderCommand and sends it to the mediator.
    // Returns a response with the created order's ID and a success message.

    public record CreateOrderRequest(OrderDto Order);
    public record CreateOrderResponse(Guid OrderId);

    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (CreateOrderRequest request, IMediator mediator) =>
            {
                var command = request.Adapt<CreateOrderCommand>();
                var orderResult = await mediator.Send(command);
                var response = orderResult.Adapt<CreateOrderResponse>();
                return Results.Created($"/orders/{response.OrderId}", response);
            })
            .WithName("CreateOrder")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Order")
            .WithDescription("Create Order");
        }
    }
}

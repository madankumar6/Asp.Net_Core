
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid ProductId, string Name, string Description, List<string> Category, decimal Price);
    public record UpdateProductResponse(bool IsSuccess);

    public class UpdateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductResponse>();
                if (response.IsSuccess)
                {
                    return Results.Ok(response);
                }
                else
                {
                    return Results.NotFound();
                }
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
        }
    }
}

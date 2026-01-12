

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid ProductId, string Name, string Description, List<string> Category, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation($"UpdateProductCommandHandler.Handle method called with the {command}");

            var product = await session.LoadAsync<Product>(command.ProductId, cancellationToken);
            
            if (product is null)
            {
                logger.LogWarning($"Product with Id {command.ProductId} not found.");
                return new UpdateProductResult(IsSuccess: false);
            }

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            
            logger.LogInformation($"Product with Id {command.ProductId} updated successfully.");
            
            return new UpdateProductResult(IsSuccess: true);
        }
    }
}

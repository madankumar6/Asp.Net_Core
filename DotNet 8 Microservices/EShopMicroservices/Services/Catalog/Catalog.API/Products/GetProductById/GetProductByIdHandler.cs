namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdResult(Product Product);
    public record GetProductByIdQuery(Guid ProductId) : ICommad<GetProductByIdResult>;

    public class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(query.ProductId, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException(query.ProductId);
            }

            return new GetProductByIdResult(product);
        }
    }
}

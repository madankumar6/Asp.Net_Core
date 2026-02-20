using ECommerce.ProductsService.Core.Entities;

namespace ECommerce.ProductsService.Core.RepositoryContracts
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Entities.Product>> GetAllProducts();
        Task<Entities.Product> GetProductById(Guid productId);
        //Task<Entities.Product> GetProductByCondition();
        Task<Entities.Product> AddProduct(Product product);
        Task<Entities.Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Guid productId);
    }
}

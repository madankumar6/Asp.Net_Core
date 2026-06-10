using ECommerce.ProductsService.Core.Entities;
using System.Linq.Expressions;

namespace ECommerce.ProductsService.Core.RepositoryContracts
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid productId);
        Task<List<Product>> GetProductsByCondition(Expression<Func<Product, bool>> predicate);
        Task<Product> GetProductByCondition(Expression<Func<Product, bool>> predicate);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Guid productId);
    }
}

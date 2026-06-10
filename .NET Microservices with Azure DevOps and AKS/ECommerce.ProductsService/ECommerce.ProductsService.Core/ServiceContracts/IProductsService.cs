using ECommerce.ProductsService.Core.Dtos.Request;
using ECommerce.ProductsService.Core.Dtos.Response;
using ECommerce.ProductsService.Core.Entities;
using System.Linq.Expressions;

namespace ECommerce.ProductsService.Core.ServiceContracts
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductResponse>> GetAllProducts();
        Task<ProductResponse> GetProductById(Guid productId);
        Task<ProductResponse> GetProductByCondition(Expression<Func<Product, bool>> predicate);
        Task<List<ProductResponse>> GetProductsByCondition(Expression<Func<Product, bool>> predicate);
        Task<ProductResponse> AddProduct(ProductAddRequest request);
        Task<ProductResponse> UpdateProduct(ProductUpdateRequest request);
        Task<bool> DeleteProduct(Guid productId);
    }
}

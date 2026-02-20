using ECommerce.ProductsService.Core.Dtos.Request;
using ECommerce.ProductsService.Core.Dtos.Response;

namespace ECommerce.ProductsService.Core.ServiceContracts
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductResponse>> GetAllProducts();
        Task<ProductResponse> GetProductById(Guid productId);
        //Task<ProductResponse> GetProductByCondition();
        Task<ProductResponse> AddProduct(ProductAddRequest request);
        Task<ProductResponse> UpdateProduct(ProductUpdateRequest request);
        Task<bool> DeleteProduct(Guid productId);
    }
}

using AutoMapper;
using ECommerce.ProductsService.Core.Dtos.Request;
using ECommerce.ProductsService.Core.Dtos.Response;
using ECommerce.ProductsService.Core.Entities;
using ECommerce.ProductsService.Core.RepositoryContracts;
using ECommerce.ProductsService.Core.ServiceContracts;
using System.Linq.Expressions;

namespace ECommerce.ProductsService.Core.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public ProductsService(IProductsRepository productsRepository, IMapper mapper)
        {
            this._productsRepository = productsRepository;
            _mapper = mapper;
        }

        public async Task<ProductResponse> GetProductById(Guid productId)
        {
            var product = await _productsRepository.GetProductById(productId);
            if (product == null)
            {
                return null;
            }
            var response = _mapper.Map<ProductResponse>(product);

            return response;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProducts()
        {
            var products = await _productsRepository.GetAllProducts();
            var response = _mapper.Map<IEnumerable<ProductResponse>>(products);

            return response;
        }

        public async Task<ProductResponse> AddProduct(ProductAddRequest request)
        {
            var product = _mapper.Map<Product>(request);
            var result = await _productsRepository.AddProduct(product);
            
            var response = _mapper.Map<ProductResponse>(result);
            return response;
        }

        public async Task<ProductResponse> UpdateProduct(ProductUpdateRequest request)
        {
            var product = _mapper.Map<Product>(request);
            var result = await _productsRepository.UpdateProduct(product);
            var response = _mapper.Map<ProductResponse>(result);

            return response;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            var result = await _productsRepository.DeleteProduct(productId);
            
            return result;
        }

        public async Task<ProductResponse> GetProductByCondition(Expression<Func<Product, bool>> predicate)
        {
            var product = await _productsRepository.GetProductByCondition(predicate);

            if (product == null)
            {
                return null;
            }

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<List<ProductResponse>> GetProductsByCondition(Expression<Func<Product, bool>> predicate)
        {
            var products = await _productsRepository.GetProductsByCondition(predicate);
            return _mapper.Map<List<ProductResponse>>(products);
        }
    }
}

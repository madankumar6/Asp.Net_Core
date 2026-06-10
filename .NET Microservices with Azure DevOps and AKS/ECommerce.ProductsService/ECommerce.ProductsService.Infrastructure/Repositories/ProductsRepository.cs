using ECommerce.ProductsService.Core.Entities;
using ECommerce.ProductsService.Core.RepositoryContracts;
using ECommerce.ProductsService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.ProductsService.Infrastructure.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.ProductID);
            if (existingProduct == null)
            {
                return null;
            }
            existingProduct.ProductName = product.ProductName;
            existingProduct.Category = product.Category;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.QuantityInStock = product.QuantityInStock;
            
            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
            
            return product;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            return product;
        }

        public async Task<List<Product>> GetProductsByCondition(Expression<Func<Product, bool>> predicate)
        {
            var products = await _context.Products.Where(predicate).ToListAsync();
            return products;
        }

        public async Task<Product> GetProductByCondition(Expression<Func<Product, bool>> predicate)
        {
            var product = await _context.Products.FirstOrDefaultAsync(predicate);
            return product; 
        }
    }
}

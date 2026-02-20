using ECommerce.ProductsService.Core.Entities;
using ECommerce.ProductsService.Core.RepositoryContracts;
using ECommerce.ProductsService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
            var existingProduct = await _context.Products.FindAsync(product.ProductId);
            if (existingProduct == null)
            {
                return null;
            }
            existingProduct.Name = product.Name;
            existingProduct.Category = product.Category;
            existingProduct.Price = product.Price;
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

        public Task<Product> GetProductByCondition()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            return product;
        }
    }
}


namespace Order.Domain.Models
{
    public class Product : Entity<ProductId>
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; } = default!;

        public static Product Create(ProductId productId, string name, decimal price)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            if (price <= 0) throw new ArgumentException("Price must be greater than zero.", nameof(price));

            var product = new Product { Id = productId, Name = name, Price = price };
            return product;
        }
    }
}

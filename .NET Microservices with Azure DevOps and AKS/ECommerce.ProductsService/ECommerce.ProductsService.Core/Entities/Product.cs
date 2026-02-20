
namespace ECommerce.ProductsService.Core.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double? Price { get; set; }
        public int? QuantityInStock { get; set; }
    }
}

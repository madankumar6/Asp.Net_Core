
namespace ECommerce.ProductsService.Core.Dtos.Request
{
    public class ProductAddRequest
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public double? Price { get; set; }
        public int? QuantityInStock { get; set; }
    }
}

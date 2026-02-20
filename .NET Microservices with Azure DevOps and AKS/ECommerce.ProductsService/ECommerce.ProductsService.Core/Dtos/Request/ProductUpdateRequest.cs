
namespace ECommerce.ProductsService.Core.Dtos.Request
{
    public class ProductUpdateRequest
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double? Price { get; set; }
        public int? QuantityInStock { get; set; }
    }
}

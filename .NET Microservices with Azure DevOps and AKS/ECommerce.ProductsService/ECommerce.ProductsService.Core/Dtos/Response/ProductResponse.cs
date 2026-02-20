
namespace ECommerce.ProductsService.Core.Dtos.Response
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double? Price { get; set; }
        public int? QuantityInStock { get; set; }
    }
}

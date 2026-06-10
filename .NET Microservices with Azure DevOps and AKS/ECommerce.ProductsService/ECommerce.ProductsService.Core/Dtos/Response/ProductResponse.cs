
namespace ECommerce.ProductsService.Core.Dtos.Response
{
    public class ProductResponse
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public double? UnitPrice { get; set; }
        public int? QuantityInStock { get; set; }
    }
}

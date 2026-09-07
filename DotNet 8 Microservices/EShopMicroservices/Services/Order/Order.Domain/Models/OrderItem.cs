
namespace Order.Domain.Models
{
    public class OrderItem : Entity<OrderItemId>
    {
        internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
        {
            Id = OrderItemId.Of(Guid.NewGuid());
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public OrderId OrderId { get; set; } = default!;
        public ProductId ProductId { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public decimal Price { get; set; } = default!;


        public static OrderItem Create(OrderId orderId, ProductId productId, int quantity, decimal price)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            if (price <= 0) throw new ArgumentException("Price must be greater than zero.", nameof(price));

            var orderItem = new OrderItem(orderId, productId, quantity, price);
            return orderItem;
        }
    }
}

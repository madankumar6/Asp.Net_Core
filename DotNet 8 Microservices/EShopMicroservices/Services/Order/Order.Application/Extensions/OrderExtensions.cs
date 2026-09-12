
namespace Order.Application.Extensions
{
    public static class OrderExtensions
    {
        public static IEnumerable<OrderDto> ProjectToOrderDtos(this IEnumerable<Domain.Models.Order> orders)
        {
            List<OrderDto> orderDtos = new List<OrderDto>();
            foreach (var item in orders)
            {
                var orderDto = DtoFromOrder(item);
                orderDtos.Add(orderDto);
            }

            return orderDtos;
        }

        public static OrderDto ToOrderDto(this Domain.Models.Order order)
        {
            return DtoFromOrder(order);
        }

        private static OrderDto DtoFromOrder(Domain.Models.Order order)
        {
            return new OrderDto(
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                OrderName: order.OrderName.Value,
                ShippingAddress: new AddressDto(
                    FirstName: order.ShippingAddress.FirstName,
                    LastName: order.ShippingAddress.LastName,
                    Email: order.ShippingAddress.Email,
                    AddressLine: order.ShippingAddress.AddressLine,
                    State: order.ShippingAddress.State,
                    Country: order.ShippingAddress.Country,
                    ZipCode: order.ShippingAddress.ZipCode
                ),
                BillingAddress: new AddressDto(
                    FirstName: order.BillingAddress.FirstName,
                    LastName: order.BillingAddress.LastName,
                    Email: order.BillingAddress.Email,
                    AddressLine: order.BillingAddress.AddressLine,
                    State: order.BillingAddress.State,
                    Country: order.BillingAddress.Country,
                    ZipCode: order.BillingAddress.ZipCode
                ),
                Payment: new PaymentDto(
                    CardName: order.Payment.CardName,
                    CardNumber: order.Payment.CardNumber,
                    Expiration: order.Payment.Expiration,
                    Cvv: order.Payment.CVV,
                    PaymentMethod: order.Payment.PaymentMethod
                ),
                Status: order.Status,
                OrderItems: order.OrderItems.Select(oi => new OrderItemDto
                    (
                        OrderId: oi.OrderId.Value,
                        ProductId: oi.ProductId.Value,
                        Quantity: oi.Quantity,
                        Price: oi.Price
                    )).ToList()
            );
        }
    }
}

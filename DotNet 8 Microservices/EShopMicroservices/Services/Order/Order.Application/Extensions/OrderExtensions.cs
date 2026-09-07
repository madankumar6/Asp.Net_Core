
namespace Order.Application.Extensions
{
    public static class OrderExtensions
    {
        public static IEnumerable<OrderDto> ProjectToOrderDtos(this IEnumerable<Domain.Models.Order> orders)
        {
            List<OrderDto> orderDtos = new List<OrderDto>();
            foreach (var item in orders)
            {
                var orderDto = new OrderDto(
                    Id: item.Id.Value,
                    CustomerId: item.CustomerId.Value,
                    OrderName: item.OrderName.Value,
                    ShippingAddress: new AddressDto(
                        FirstName: item.ShippingAddress.FirstName,
                        LastName: item.ShippingAddress.LastName,
                        Email: item.ShippingAddress.Email,
                        AddressLine: item.ShippingAddress.AddressLine,
                        State: item.ShippingAddress.State,
                        Country: item.ShippingAddress.Country,
                        ZipCode: item.ShippingAddress.ZipCode
                    ),
                    BillingAddress: new AddressDto(
                        FirstName: item.BillingAddress.FirstName,
                        LastName: item.BillingAddress.LastName,
                        Email: item.BillingAddress.Email,
                        AddressLine: item.BillingAddress.AddressLine,
                        State: item.BillingAddress.State,
                        Country: item.BillingAddress.Country,
                        ZipCode: item.BillingAddress.ZipCode
                    ),
                    Payment: new PaymentDto(
                        CardName: item.Payment.CardName,
                        CardNumber: item.Payment.CardNumber,
                        Expiration: item.Payment.Expiration,
                        Cvv: item.Payment.CVV,
                        PaymentMethod: item.Payment.PaymentMethod
                    ),
                    Status: item.Status,
                    OrderItems: item.OrderItems.Select(oi => new OrderItemDto
                        (
                            OrderId: oi.OrderId.Value,
                            ProductId: oi.ProductId.Value,
                            Quantity: oi.Quantity,
                            Price: oi.Price
                        )).ToList()
                    );
                orderDtos.Add(orderDto);
            }

            return orderDtos;
        }
    }
}

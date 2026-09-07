
namespace Order.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            //Update the existing order based on the request.Order data
            //var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id.Value == command.Order.Id, cancellationToken);
            var orderId = OrderId.Of(command.Order.Id);
            var order = await dbContext.Orders.FindAsync(orderId, cancellationToken);

            if (order == null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }

            // Update the order properties
            UpdateOrderWithNewValues(order, command.Order);
            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }

        private void UpdateOrderWithNewValues(Domain.Models.Order order, OrderDto orderDto)
        {
            // Implementation for updating order properties
            var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.Email, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.State, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.ZipCode);
            var billingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.Email, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.State, orderDto.BillingAddress.Country, orderDto.BillingAddress.ZipCode);
            var payment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

            order.Update(OrderName.Of(orderDto.OrderName), shippingAddress, billingAddress, payment, orderDto.Status);
        }
    }
}

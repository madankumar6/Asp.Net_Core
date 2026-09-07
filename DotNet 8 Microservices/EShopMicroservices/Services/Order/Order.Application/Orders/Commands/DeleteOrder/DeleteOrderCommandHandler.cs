
namespace Order.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            //Delete the existing order based on the request.Order data
            var orderId = OrderId.Of(command.OrderId);
            var order = await dbContext.Orders.FindAsync(orderId, cancellationToken);

            if (order == null)
            {
                throw new OrderNotFoundException(command.OrderId);
            }

            // Delete the order
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteOrderResult(true);
        }
    }
}

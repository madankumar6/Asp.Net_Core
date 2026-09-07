
namespace Order.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.Pagination.PageIndex;
            var pageSize = query.Pagination.PageSize;
            var totalCount = await context.Orders.LongCountAsync(cancellationToken);
            var orders = await context.Orders
                .Include(o => o.OrderItems)
                .OrderBy(o => o.OrderName.Value)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var orderDtos = orders.ProjectToOrderDtos();

            var paginatedResult = new PaginatedResult<OrderDto>(pageIndex, pageSize, totalCount, orderDtos);
            return new GetOrdersResult(paginatedResult);
        }
    }
}

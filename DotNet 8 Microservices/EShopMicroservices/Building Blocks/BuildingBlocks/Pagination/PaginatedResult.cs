
namespace BuildingBlocks.Pagination
{
    public class PaginatedResult<TEntity>(int pageIndex, int pageSize, long totalCount, IEnumerable<TEntity> items)
        where TEntity : class
    {
        public int PageIndex { get; set; } = pageIndex;
        public int PageSize { get; set; } = pageSize;
        public long TotalCount { get; set; } = totalCount;
        public IEnumerable<TEntity> Items { get; set; } = items;
    }
}

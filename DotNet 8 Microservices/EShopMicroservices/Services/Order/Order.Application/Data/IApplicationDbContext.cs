

using Order.Domain.Models;

namespace Order.Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Domain.Models.Order> Orders { get; }
        DbSet<Product> Products { get; }
        DbSet<OrderItem> OrderItems { get; }
        DbSet<Customer> Customers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

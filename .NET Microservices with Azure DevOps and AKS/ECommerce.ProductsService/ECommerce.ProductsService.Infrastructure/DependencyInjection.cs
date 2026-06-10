using ECommerce.ProductsService.Core.RepositoryContracts;
using ECommerce.ProductsService.Infrastructure.Data;
using ECommerce.ProductsService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.ProductsService.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStringTemplate = configuration.GetConnectionString("MySqlConnection")!;
            var connectionString = connectionStringTemplate
                                .Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST")!)
                                .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD")!);
            // Register DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL(connectionString));
            // Register repositories
            services.AddScoped<IProductsRepository, ProductsRepository>();
            // Register other infrastructure services as needed
        }
    }
}

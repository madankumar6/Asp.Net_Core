using ECommerce.UserService.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.UserService.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Extension method to add infrastructure services to the IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Register infrastructure services here
            services.AddScoped<Core.RepositoryContracts.IUserRepository, Repositories.UserRepository>();
            services.AddTransient<DapperDbContext>();

            return services;
        }
    }
}

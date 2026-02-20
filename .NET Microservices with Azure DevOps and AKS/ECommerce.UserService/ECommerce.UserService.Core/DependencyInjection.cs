using ECommerce.UserService.Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.UserService.Core
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Extension method to add infrastructure services to the IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            // Register infrastructure services here
            services.AddScoped<Core.ServiceContracts.IUserService, Services.UserService>();
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

            return services;
        }
    }
}

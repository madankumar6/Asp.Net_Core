using ECommerce.ProductsService.Core.ServiceContracts;
using ECommerce.ProductsService.Core.Validators;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace ECommerce.ProductsService.Core
{
    public static class DependencyInjection
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            // Register services
            services.AddScoped<IProductsService, Services.ProductsService>();
            services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();

            // Register other core services as needed
        }
    }
}

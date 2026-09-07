using BuildingBlocks.Exceptions.Handler;
using Carter;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Order.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register API services here
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();
            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("Database")!, name: "SQL Server", tags: new[] { "db", "sql", "sqlserver" });
            services.AddCarter();

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            // This will ensure that the exception handler middleware is registered and will handle exceptions globally.
            app.UseExceptionHandler(options => { });

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.MapCarter();

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains("db"),
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var result = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(entry => new
                        {
                            name = entry.Key,
                            status = entry.Value.Status.ToString(),
                            exception = entry.Value.Exception?.Message,
                            duration = entry.Value.Duration.ToString()
                        })
                    });
                    await context.Response.WriteAsync(result);
                }
            });

            return app;
        }
    }
}

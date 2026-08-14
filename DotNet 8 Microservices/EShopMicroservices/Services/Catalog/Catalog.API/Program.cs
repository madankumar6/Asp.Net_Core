using BuildingBlocks.Exceptions.Handler;
using Catalog.API.Data;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

//Add services to the container.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.MapGet("/", () => "Hello World!");
app.MapCarter();

// This will ensure that the exception handler middleware is registered and will handle exceptions globally.
app.UseExceptionHandler(options => { });

//app.UseExceptionHandler(exceptionHandlerApp =>
//{
//    exceptionHandlerApp.Run(async context =>
//    {
//        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

//        if (exception is null)
//        {
//            return;
//        }

//        var problemDetails = new ProblemDetails
//        {
//            Title = exception.Message,
//            Status = StatusCodes.Status500InternalServerError,
//            Detail = exception.StackTrace
//        };

//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exception, exception.Message);

//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType = "application/problem+json";

//        await context.Response.WriteAsJsonAsync(problemDetails);
//    });
//});

app.UseHealthChecks("/health", 
    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions 
    { 
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();

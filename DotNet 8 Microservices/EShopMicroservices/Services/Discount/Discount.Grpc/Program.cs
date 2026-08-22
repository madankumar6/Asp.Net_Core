using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Services.AddDbContext<DiscountContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});

var app = builder.Build();

app.UseMigration();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<DiscountService>();

// Map the reflection service endpoint (usually only in development)
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService(); // <--- ADD THIS LINE for discovering the proto information in Postman tool.
}

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

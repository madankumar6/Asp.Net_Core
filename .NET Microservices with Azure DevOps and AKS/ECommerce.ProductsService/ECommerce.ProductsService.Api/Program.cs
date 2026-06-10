using ECommerce.ProductsService.Api.ApiEndPoints;
using ECommerce.ProductsService.Api.Middlewares;
using ECommerce.ProductsService.Core;
using ECommerce.ProductsService.Core.Dtos.Request;
using ECommerce.ProductsService.Core.MappingProfiles;
using ECommerce.ProductsService.Core.ServiceContracts;
using ECommerce.ProductsService.Infrastructure;
using System.Web.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCoreServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add controllers if needed
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

builder.Services.AddAutoMapper(config => {
}, typeof(ProductMappingProfile).Assembly);

//Add API explorer services
builder.Services.AddEndpointsApiExplorer();

// Add swagger generation services to create swagger specification
builder.Services.AddSwaggerGen();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// Add model binder to read values from JSON to enum parameters
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseExceptionHandlingMiddleware();

//Enable routing
app.UseRouting();

// Enable swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

//Enable authorization if needed
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapProductApiEndPoints();

//app.MapGet("/products", (IProductsService productsService) =>
//{
//    var products = productsService.GetAllProducts();
//    return products;
//});

//app.MapGet("/products/{productId}", (IProductsService productsService, Guid productId) =>
//{
//    var product = productsService.GetProductById(productId);
//    return product;
//});

//app.MapPost("/products", (IProductsService productsService, [FromBody]ProductAddRequest product) =>
//{
//    var productResponse = productsService.AddProduct(product);
//    return productResponse;
//});

//app.MapPut("/products", (IProductsService productsService, [FromBody] ProductUpdateRequest product) =>
//{
//    var productResponse = productsService.UpdateProduct(product);
//    return productResponse;
//});

//app.MapDelete("/products/{productId}", (IProductsService productsService, Guid productId) =>
//{
//    var isSuccess = productsService.DeleteProduct(productId);
//    return isSuccess;
//});

app.Run();

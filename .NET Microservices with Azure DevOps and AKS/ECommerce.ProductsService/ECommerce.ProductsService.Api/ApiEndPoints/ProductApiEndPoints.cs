using ECommerce.ProductsService.Core.Dtos.Request;
using ECommerce.ProductsService.Core.ServiceContracts;
using ECommerce.ProductsService.Core.Validators;
using FluentValidation;
using System.Web.Http;

namespace ECommerce.ProductsService.Api.ApiEndPoints
{
    public static class ProductApiEndPoints
    {
        public static IEndpointRouteBuilder MapProductApiEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/products", async (IProductsService productsService) =>
            {
                var products = await productsService.GetAllProducts();
                return Results.Ok(products);
            });

            app.MapGet("/api/products/{productId:guid}", async (IProductsService productsService, Guid productId) =>
            {
                var product = await productsService.GetProductById(productId);
                return Results.Ok(product);
            });

            app.MapGet("/api/products/search/product-id/{productId:guid}", async (IProductsService productsService, Guid productId) =>
            {
                var product = await productsService.GetProductByCondition(x => x.ProductID == productId);
                return Results.Ok(product);
            });

            app.MapGet("/api/products/search/{searchString}", async (IProductsService productsService, string searchString) =>
            {
                var productsByName = await productsService.GetProductsByCondition(x => x.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
                var productsByCategory = await productsService.GetProductsByCondition(x => x.Category.ToString() == searchString);

                return Results.Ok(productsByName.Union(productsByCategory));
            });

            app.MapPost("/api/products", async (IProductsService productsService, IValidator<ProductAddRequest> validator, [FromBody] ProductAddRequest product) =>
            {
                var validationResult = await validator.ValidateAsync(product);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                    return Results.BadRequest(errors);
                }

                var productResponse = await productsService.AddProduct(product);
                return Results.Created($"api/products/search/product-id/{productResponse.ProductID}", productResponse);
            });

            app.MapPut("/api/products", async (IProductsService productsService, IValidator<ProductUpdateRequest> validator, [FromBody] ProductUpdateRequest product) =>
            {
                var validationResult = await validator.ValidateAsync(product);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors
                                    .GroupBy(x => x.PropertyName)
                                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                    return Results.ValidationProblem(errors);
                }
                var productResponse = await productsService.UpdateProduct(product);
                return Results.Ok(productResponse);
            });

            app.MapDelete("/api/products/{productId:guid}", async (IProductsService productsService, Guid productId) =>
            {
                var isSuccess = await productsService.DeleteProduct(productId);
                if (isSuccess)
                {
                    return Results.Ok(isSuccess);
                }
                else
                {
                    return Results.Problem("Unable to delete the product");
                }
            });

            return app;
        }
    }
}

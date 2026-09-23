using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class ProductDetailModel(ICatalogService catalogService,
        IBasketService basketService, ILogger<ProductDetailModel> logger) : PageModel
    {
        public ProductModel Product { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string Color { get; set; } = "Black"; // Default color, can be modified as needed

        [BindProperty(SupportsGet = true)]
        public string Quantity { get; set; } = default!; // Default quantity, can be modified as needed

        public async Task<IActionResult> OnGetAsync(Guid productId)
        {
            var product = (await catalogService.GetProduct(productId)).Product;
            Product = product;

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            logger.LogInformation("Add to basket requested for productId: {ProductId}", productId);
            var product = (await catalogService.GetProduct(productId)).Product;

            ShoppingCartModel basket = await basketService.LoadUserBasket();
            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1,
                Color = "Black" // Default color, can be modified as needed
            });

            await basketService.StoreBasket(new StoreBasketRequest(basket));

            return RedirectToPage("Cart");
        }
    }
}

namespace Shopping.Web.Pages
{
    public class IndexModel(ICatalogService catalogService, IBasketService basketService, ILogger<IndexModel> logger) : PageModel
    {
        public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation("Index page requested.");
            var result = await catalogService.GetProducts();
            ProductList = result.Products ?? new List<ProductModel>();
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

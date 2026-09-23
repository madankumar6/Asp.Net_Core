namespace Shopping.Web.Pages
{
    public class ProductListModel(ICatalogService catalogService, 
        IBasketService basketService, ILogger<ProductListModel> logger) : PageModel
    {
        public IEnumerable<string> CategoryList { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<ProductModel> ProductList { get; set; } = Enumerable.Empty<ProductModel>();
        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(string category)
        {
            var products = (await catalogService.GetProducts()).Products;
            CategoryList = products.SelectMany(p => p.Category).Distinct().ToList();

            if (!string.IsNullOrEmpty(category))
            {
                SelectedCategory = category;
                products = products.Where(p => p.Category.Contains(category)).ToList();
                ProductList = products;
            }
            else
            {
                ProductList = products;
                SelectedCategory = string.Empty;
            }

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


namespace Shopping.Web.Pages
{
    public class CartModel(IBasketService basketService, ICatalogService catalogService, ILogger<CartModel> logger) : PageModel
    {
        public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

        public async Task OnGetAsync()
        {
            var userName = "madankumar6"; // Replace with actual user identification logic
            Cart = await basketService.LoadUserBasket();
        }

        public async Task<IActionResult> OnPostRemoveFromCartAsync(Guid productId)
        {
            logger.LogInformation("Remove from basket requested for productId: {ProductId}", productId);
            var product = (await catalogService.GetProduct(productId)).Product;

            ShoppingCartModel cart = await basketService.LoadUserBasket();
            cart.Items.RemoveAll(item => item.ProductId == product.Id);
            await basketService.StoreBasket(new StoreBasketRequest(cart));

            return RedirectToPage();
        }
    }
}

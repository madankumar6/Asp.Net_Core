using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class CheckoutModel(IBasketService basketService, ILogger<CheckoutModel> logger) : PageModel
    {
        public ShoppingCartModel Cart { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public BasketCheckoutModel Order { get; set; } = default!;

        public async Task<IActionResult> OnGet(string userName)
        {
            Cart = await basketService.LoadUserBasket();

            return Page();
        }

        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            logger.LogInformation("Checkout requested for user: {UserName}", Order.UserName);
            Cart = await basketService.LoadUserBasket();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // assumption customerId is passed in from the UI authenticated user madankumar6        
            Order.CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");
            Order.UserName = Cart.UserName;
            Order.TotalPrice = Cart.TotalPrice;

            await basketService.CheckoutBasket(new CheckoutBasketRequest(Order));
         
            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
    }
}

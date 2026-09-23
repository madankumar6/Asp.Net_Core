using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class OrderListModel(IOrderService orderService, ILogger<OrderListModel> logger) : PageModel
    {
        public IEnumerable<OrderModel> Orders { get; set; } = new List<OrderModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            var customerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa"); // Replace with actual customer ID
            Orders = (await orderService.GetOrdersByCustomer(customerId)).Orders;
            
            return Page();
        }
    }
}

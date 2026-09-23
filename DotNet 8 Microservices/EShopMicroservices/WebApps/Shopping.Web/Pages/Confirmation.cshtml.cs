using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; } = "Thank you for your order! Your order has been successfully processed.";
        
        public void OnGet()
        {
            Message = "Thank you for your order! Your order has been successfully processed.";
        }

        public void OnGetOrderSubmitted()
        {
            Message = "Your order has been submitted successfully!";
        }
    }
}

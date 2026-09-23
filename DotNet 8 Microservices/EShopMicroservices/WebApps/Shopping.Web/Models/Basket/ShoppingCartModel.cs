using Shopping.Web.Models.Catalog;

namespace Shopping.Web.Models.Basket
{
    public class ShoppingCartModel
    {
        public string UserName { get; set; } = default!;
        public List<ShoppingCartItemModel> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);

        public ShoppingCartModel(string userName)
        {
            UserName = userName;
            Items = new List<ShoppingCartItemModel>();
        }

        //Required for mapping
        public ShoppingCartModel()
        {
        }
    }

    //Wrapper classes
    public record GetBasketResponse(ShoppingCartModel ShoppingCart);
    public record StoreBasketRequest(ShoppingCartModel ShoppingCart);
    public record StoreBasketResponse(string UserName);
    public record DeleteBasketResponse(bool IsSuccess);
}


namespace Shopping.Web.Services
{
    public interface IBasketService
    {
        [Get("/basket-service/basket/{userName}")]
        Task<GetBasketResponse> GetBasket(string userName);

        [Post("/basket-service/basket")]
        Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

        [Delete("/basket-service/basket/{userName}")]
        Task<DeleteBasketResponse> DeleteBasket(string userName);

        [Post("/basket-service/basket/checkout")]
        Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);

        public async Task<ShoppingCartModel> LoadUserBasket()
        {
            var userName = "madankumar6";
            ShoppingCartModel basket = null;

            try
            {
                var basketResponse = await GetBasket(userName);
                basket = basketResponse.ShoppingCart ?? new ShoppingCartModel { UserName = userName, Items = new List<ShoppingCartItemModel>() };
            }
            catch (ApiException apiException) when (apiException.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                basket = new ShoppingCartModel { UserName = userName, Items = new List<ShoppingCartItemModel>() };
            }

            return basket;
        }
    }
}

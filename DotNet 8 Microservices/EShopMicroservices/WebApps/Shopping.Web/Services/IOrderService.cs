namespace Shopping.Web.Services
{
    public interface IOrderService
    {
        [Get("/order-service/orders?pageNumber={pageNumber}&pageSize={pageSize}")]
        Task<GetOrdersResponse> GetOrders(int pageNumber = 1, int pageSize = 10);

        [Get("/order-service/orders/{orderName}")]
        Task<GetOrdersByNameResponse> GetOrdersByName(string orderName);

        [Get("/order-service/orders/customer/{customerId}")]
        Task<GetOrdersByCustomerResponse> GetOrdersByCustomer(Guid customerId);
    }
}

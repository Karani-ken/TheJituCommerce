using Client_Side.Models.Cart;
using Client_Side.Models.Orders;

namespace Client_Side.Services.Orders
{
    public interface IOrderService
    {

        Task<OrderHeaderDto> CreateOrder(CartDto cart);

        Task<StripeRequestDto> CreateStripe(StripeRequestDto stripe);

        Task<bool> ValidatePayment(Guid orderId);

        Task<List<OrderHeaderDto>> GetAllOrders(string userId = "");

        Task<OrderHeaderDto> GetOrderByID(Guid orderId);

        Task<OrderHeaderDto> updateOrder(Guid orderId, string status);


    }
}

using TheJituEcommerce_OrderService.Models.Dtos;

namespace TheJituEcommerce_OrderService.Services.IService
{
    public interface IOrderInterface
    {
        Task<OrderHeaderDto> CreateOrderHeader(CartDto cartDto);

        Task<StripeRequestDto> StripePayment(StripeRequestDto stripeRequestDto);

        Task<bool> ValidatePayment(Guid OrderId);

		Task<IEnumerable<OrderHeaderDto>> Getorders(string? UserId = "");

		Task<OrderHeaderDto> Getorder(Guid id);

		Task<OrderHeaderDto> updateOrder(Guid orderId, string newStatus);
	}
}

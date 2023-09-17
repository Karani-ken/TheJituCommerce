using TheJituEcommerce_OrderService.Models.Dtos;

namespace TheJituEcommerce_OrderService.Services.IService
{
    public interface IOrderInterface
    {
        Task<OrderHeaderDto> CreateOrderHeader(CartDto cartDto);
    }
}

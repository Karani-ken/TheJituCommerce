using TheJitu_Ecommerce_Cart.Models.Dtos;

namespace TheJitu_Ecommerce_Cart.Services.IServices
{
    public interface ICartService
    {
        Task<bool> CartUpsert(CartDto cartDto);

        Task<CartDto> GetUserCart(Guid userId);

        Task<bool> ApplyCoupons(CartDto cartDto);

        Task<bool> RemoveFromCart(Guid CartDetailId);
    }
}

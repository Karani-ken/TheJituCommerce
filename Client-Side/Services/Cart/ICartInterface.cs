using Client_Side.Models.Cart;
using Client_Side.Models;
namespace Client_Side.Services.Cart
{
    public interface ICartInterface
    {
        Task<ResponseDto> AddToCart(CartDto cartDto);

        Task<CartDto> GetCartByUserId(Guid userId);

        Task<ResponseDto> ApplyCoupons(CartDto cartDto);


        Task<ResponseDto> RemoveFromCart(Guid cartDetailId);

        Task<bool> CartDelete(Guid userId);
    }
}

using TheJitu_Ecommerce_Cart.Models.Dtos;

namespace TheJitu_Ecommerce_Cart.Services.IServices
{
    public interface IProductInterface
    {
        Task<IEnumerable<ProductDto>> GetProductaAsync();
    }
}

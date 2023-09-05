namespace TheJitu_Ecommerce_Cart.Models.Dtos
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }

        public IEnumerable<CartDetailsDto>? CartDetails { get; set; }
    }
}

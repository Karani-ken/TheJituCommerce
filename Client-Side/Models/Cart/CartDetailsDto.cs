using Client_Side.Models.Products;

namespace Client_Side.Models.Cart
{
    public class CartDetailsDto
    {
        public Guid CartDetailsId { get; set; }
        public Guid CartHeaderId { get; set; }
        public CartHeaderDto? CartHeaderDto { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
    }
}

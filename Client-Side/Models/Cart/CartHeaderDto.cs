namespace Client_Side.Models.Cart
{
    public class CartHeaderDto
    {
        public Guid CartHeaderId { get; set; }
        public Guid UserId { get; set; }
        public string? CouponCode { get; set; } = string.Empty;
        public int Discount { get; set; }
        public int CartTotal { get; set; }
    }
}

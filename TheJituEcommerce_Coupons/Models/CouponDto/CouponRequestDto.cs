namespace TheJituEcommerce_Coupons.Models.CouponDto
{
    public class CouponRequestDto
    {
        public string CouponCode { get; set; } = string.Empty;

        public int CouponAmount { get; set; }

        public int CouponMinAmont { get; set; }
    }
}

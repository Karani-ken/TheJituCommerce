using System.ComponentModel.DataAnnotations;

namespace Coupons_Service.Data
{
    public class Coupon
    {
        [Key]
        public Guid CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; } = string.Empty;
        [Required]
        public int CouponAmount { get; set; }
        [Required]
        public int CouponMinAmont { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

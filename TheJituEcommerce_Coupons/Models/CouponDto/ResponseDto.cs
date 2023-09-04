using System.ComponentModel.DataAnnotations;

namespace TheJituEcommerce_Coupons.Models.CouponDto
{
    public class ResponseDto
    {
        public object? Result { get; set; }

        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; } = string.Empty;
    }
}

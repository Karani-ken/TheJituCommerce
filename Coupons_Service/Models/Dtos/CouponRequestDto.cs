﻿namespace Coupons_Service.Data.Dtos
{
    public class CouponRequestDto
    {
        public string CouponCode { get; set; } = string.Empty;

        public int CouponAmount { get; set; }

        public int CouponMinAmont { get; set; }
    }
}

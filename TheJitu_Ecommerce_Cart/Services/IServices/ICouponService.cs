﻿using TheJitu_Ecommerce_Cart.Models.Dtos;

namespace TheJitu_Ecommerce_Cart.Services.IServices
{
    public interface ICouponService
    {
        Task<CouponDto> GetCouponData(string CouponCode);
    }
}

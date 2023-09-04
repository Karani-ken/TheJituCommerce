using TheJituEcommerce_Coupons.Models.CouponDto;
using TheJituEcommerce_Coupons.Models;
using AutoMapper;

namespace TheJituEcommerce_Coupons.Profiles
{
    public class CouponProfiles:Profile
    {
        public CouponProfiles() 
        {
            CreateMap<CouponRequestDto, Coupon>().ReverseMap();

        }
    }
}

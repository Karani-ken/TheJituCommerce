using AutoMapper;
using TheJitu_Ecommerce_Cart.Models;
using TheJitu_Ecommerce_Cart.Models.Dtos;

namespace TheJitu_Ecommerce_Cart.Profiles
{
    public class CartProfiles:Profile
    {
        public CartProfiles()
        {
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
        }
      
    }
}

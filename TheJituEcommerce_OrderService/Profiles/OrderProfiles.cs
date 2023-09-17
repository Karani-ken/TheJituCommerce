using AutoMapper;
using TheJituEcommerce_OrderService.Models;
using TheJituEcommerce_OrderService.Models.Dtos;

namespace TheJituEcommerce_OrderService.Profiles
{
    public class OrderProfiles:Profile
    {
        public OrderProfiles()
        {
            CreateMap<CartHeaderDto,OrderHeaderDto>()
                .ForMember(dest=> dest.OrderTotal, src=> src.MapFrom(x=>x.CartTotal)).ReverseMap();
            CreateMap<CartDetailsDto, OrderDetailsDto>()
                .ForMember(dest => dest.ProductName, src => src.MapFrom(x => x.Product.Name))
                .ForMember(dest => dest.Price, src => src.MapFrom(x => x.Product.Price));

            CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();
        }
    }
}

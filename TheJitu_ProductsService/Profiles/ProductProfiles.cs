using AutoMapper;
using TheJitu_ProductsService.Models.Dtos;
using TheJitu_ProductsService.Models;

namespace TheJitu_ProductsService.Profiles
{
    public class ProductProfiles:Profile
    {
        public ProductProfiles()
        {
            CreateMap<ProductRequestDto, Product>().ReverseMap();
        }
    }
}

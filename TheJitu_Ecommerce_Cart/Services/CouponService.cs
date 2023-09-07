using Newtonsoft.Json;
using TheJitu_Ecommerce_Cart.Models.Dtos;
using TheJitu_Ecommerce_Cart.Services.IServices;

namespace TheJitu_Ecommerce_Cart.Services
{
    public class CouponService : ICouponService
    { 
        private readonly IHttpClientFactory _httpClientFactory;
        public CouponService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
                
        }
        public async Task<CouponDto> GetCouponData(string CouponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/Coupons/GetByName/{CouponCode}");
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result));
            }
            return new CouponDto();
           
        }
    }
}

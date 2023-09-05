using Newtonsoft.Json;
using TheJitu_Ecommerce_Cart.Models.Dtos;
using TheJitu_Ecommerce_Cart.Services.IServices;

namespace TheJitu_Ecommerce_Cart.Services
{
    public class ProductService : IProductInterface
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<ProductDto>> GetProductaAsync()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync("/api/Prodcut");
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(responseDto.Result));
            }
            return new List<ProductDto>();
        }
    }
}

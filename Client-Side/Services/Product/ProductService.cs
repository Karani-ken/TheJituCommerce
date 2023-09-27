using Client_Side.Models;
using Client_Side.Models.Products;
using Newtonsoft.Json;
using System.Text;

namespace Client_Side.Services.Product
{
    public class ProductService : IProductInterface
    {
        private readonly HttpClient _httpClient;
        private readonly string BASEURL = "https://localhost:7117";
        public ProductService(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }
        public async Task<ResponseDto> AddProduct(ProductRequestDto product)
        {
            var request = JsonConvert.SerializeObject(product);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BASEURL}/api/Product", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                //change this to a list of products
                return results;

            }

            return new ResponseDto();
        }

        public async Task<ResponseDto> deleteProduct(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{BASEURL}/api/Product?id={id}");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                //change this to a list of products
                return results;

            }

            return new ResponseDto();
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Product/GetById/{id}");
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<ProductDto>(results.Result.ToString());

            }
            return new ProductDto();
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Product");
            var content = await response.Content.ReadAsStringAsync();


            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<List<ProductDto>>(results.Result.ToString());

            }
            return new List<ProductDto>();
        }

        public async Task<ResponseDto> updateProduct(Guid id, ProductRequestDto productRequestDto)
        {
            var request = JsonConvert.SerializeObject(productRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BASEURL}/api/Product?id={id}", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                //change this to a list of products
                return results;

            }

            return new ResponseDto();
        }
    }
}

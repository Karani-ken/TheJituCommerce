using Client_Side.Models;
using Client_Side.Models.Cart;
using Newtonsoft.Json;
using System.Text;

namespace Client_Side.Services.Cart
{
    public class CartService : ICartInterface
    {
        private readonly HttpClient _httpClient;

        private readonly string BASEURL = "";
        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }

        public async Task<ResponseDto> AddToCart(CartDto cartDto)
        {
            var request = JsonConvert.SerializeObject(cartDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/Cart", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return results;

            }
            return new ResponseDto();
        }

        public async Task<ResponseDto> ApplyCoupons(CartDto cartDto)
        {
            var request = JsonConvert.SerializeObject(cartDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PutAsync($"{BASEURL}/api/Cart", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return results;

            }
            return new ResponseDto();
        }

        public async Task<bool> CartDelete(Guid userId)
        {
            //communicate wih Api

            var response = await _httpClient.DeleteAsync($"{BASEURL}/api/Cart/deleteFromCart?userId={userId}");
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return true;

            }
            return false;
        }

        public async Task<CartDto> GetCartByUserId(Guid userId)
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Cart?userId={userId}");
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<CartDto>(results.Result.ToString());

            }
            return new CartDto();
        }

        public async Task<ResponseDto> RemoveFromCart(Guid cartDetailId)
        {
            var response = await _httpClient.DeleteAsync($"{BASEURL}/api/Cart?cartDetailsId={cartDetailId}");
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

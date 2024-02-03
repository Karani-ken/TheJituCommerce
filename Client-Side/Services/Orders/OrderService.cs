using Client_Side.Models.Cart;
using Client_Side.Models.Orders;
using Newtonsoft.Json;
using System.Text;
using Client_Side.Models;

namespace Client_Side.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        //private readonly string BASEURL = "https://thejitucommercegatewayapi.azurewebsites.net";
        private readonly string BASEURL = "https://localhost:7132";
        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderHeaderDto> CreateOrder(CartDto cart)
        {
            var request = JsonConvert.SerializeObject(cart);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/Order", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<OrderHeaderDto>(results.Result.ToString());
            }
            return new OrderHeaderDto();
        }

        public async Task<StripeRequestDto> CreateStripe(StripeRequestDto stripe)
        {
            var request = JsonConvert.SerializeObject(stripe);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/Order/StripePayment", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<StripeRequestDto>(results.Result.ToString());
            }
            return new StripeRequestDto();
        }

        public async Task<List<OrderHeaderDto>> GetAllOrders(string userId)
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Order/Getorders?userId={userId}");
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<List<OrderHeaderDto>>(results.Result.ToString()); ;
            }

            return new List<OrderHeaderDto>();
        }

        public async Task<OrderHeaderDto> GetOrderByID(Guid orderId)
        {


            var response = await _httpClient.GetAsync($"{BASEURL}/api/Order/Getorder/id?id={orderId}");
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<OrderHeaderDto>(results.Result.ToString()); ;
            }

            return new OrderHeaderDto();
        }

        public async Task<OrderHeaderDto> updateOrder(Guid orderId, string status)
        {

            var response = await _httpClient.PutAsync($"{BASEURL}/api/Order/Getorders?orderId={orderId}&orderStatus={status}", null);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<OrderHeaderDto>(results.Result.ToString()); ;
            }

            return new OrderHeaderDto();
        }

        public async Task<bool> ValidatePayment(Guid orderId)
        {

            var response = await _httpClient.PostAsync($"{BASEURL}/api/Order/validatePayment?orderId={orderId}", null);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return true;
            }
            return false;
        }
    }
}

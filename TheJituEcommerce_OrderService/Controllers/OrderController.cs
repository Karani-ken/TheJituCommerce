using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheJituEcommerce_OrderService.Models.Dtos;
using TheJituEcommerce_OrderService.Services.IService;

namespace TheJituEcommerce_OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderInterface _orderService;
        private readonly ResponseDto _responseDto;
        public OrderController(IOrderInterface orderInterface)
        {
            _responseDto = new ResponseDto();
            _orderService= orderInterface;
        }
        [HttpPost]
        public async Task <ActionResult<ResponseDto>> AddOrder(CartDto cartDto)
        {
            try
            {
                var response = await _orderService.CreateOrderHeader(cartDto);
                if (response != null)
                {
                    _responseDto.IsSuccess = true;
                    _responseDto.Result = response;
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                 _responseDto.Message = ex.InnerException.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
        //stripe payment
        [HttpPost("StripePayment")]
        public async Task<ActionResult<ResponseDto>> StripePayment(StripeRequestDto stripeRequestDto)
        {
            try
            {
                var response = await _orderService.StripePayment(stripeRequestDto);
                _responseDto.Result = response;
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
        //validatePayment
        [HttpPost("ValidatePayment")]
        public async Task<ActionResult<ResponseDto>> ValidatePayment(Guid OrderId)
        {
            try
            {
                var response = await _orderService.ValidatePayment(OrderId);
                _responseDto.Result = response;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
    }
}

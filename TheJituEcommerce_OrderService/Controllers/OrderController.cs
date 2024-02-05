using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("Getorders")]
        [Authorize]
        public async Task <ActionResult<ResponseDto>> Get(string? userId)
        {
			try
			{
				if (User.IsInRole("Admin"))
				{
					var res = await _orderService.Getorders();
					_responseDto.Result = res;
				}
				else
				{
					var response = await _orderService.Getorders(userId);
					_responseDto.Result = response;
				}
			}
			catch (Exception ex)
			{
				_responseDto.IsSuccess = false;
				_responseDto.Message = ex.Message;
				return BadRequest(_responseDto);
			}
			return Ok(_responseDto);
		}

		[HttpPut("Getorders")]
		[Authorize]
		public async Task<ActionResult<ResponseDto>> updateOrder(Guid orderId, string orderStatus)
		{
			try
			{
				var res = await _orderService.updateOrder(orderId, orderStatus);
				_responseDto.Result = res;
			}
			catch (Exception ex)
			{
				_responseDto.IsSuccess = false;
				_responseDto.Message = ex.Message;
				return BadRequest(_responseDto);
			}
			return Ok(_responseDto);
		}
		[HttpGet("Getorder/id")]
		[Authorize]
		public async Task<ActionResult<ResponseDto>> Getorder(Guid id)
		{
			try
			{
				var response = await _orderService.Getorder(id);
				_responseDto.Result = response;
			}
			catch (Exception ex)
			{
				_responseDto.IsSuccess = false;
				_responseDto.Message = ex.Message;
				return BadRequest(_responseDto);
			}
			return Ok(_responseDto);
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheJitu_Ecommerce_Cart.Models.Dtos;
using TheJitu_Ecommerce_Cart.Services.IServices;

namespace TheJitu_Ecommerce_Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ResponseDto _responseDto;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
            _responseDto = new ResponseDto();
        }
        //add items to cart

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CartUpsert(CartDto cartDto)
        {
            try
            {
                var response = await _cartService.CartUpsert(cartDto);
                if (response)
                {
                    _responseDto.IsSuccess = true;
                    _responseDto.Result = response;
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }

            }catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
        //remove from Cart
        [HttpDelete]
        public async Task<ActionResult<ResponseDto>> RemoveFromCart([FromBody] Guid cartDetailsId)
        {
            try
            {
                var response = await _cartService.RemoveFromCart(cartDetailsId);
               
              _responseDto.Result = response;
                
            }catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
        [HttpPut]
        //Apply Coupon to cart
        public async Task<ActionResult<ResponseDto>> ApplyCoupon(CartDto cartDto)
        {
            try
            {
                var response = await _cartService.ApplyCoupons(cartDto);
                _responseDto.Result = response;
            }catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
        //get user cart
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetUserCart(Guid userId)
        {
            try
            {
                var response = await _cartService.GetUserCart(userId);
                _responseDto.Result = response;
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheJituEcommerce_Coupons.Models;
using TheJituEcommerce_Coupons.Models.CouponDto;
using TheJituEcommerce_Coupons.Services.IService;

namespace TheJituEcommerce_Coupons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICouponInterface _couponInterface;
        private readonly ResponseDto _responseDto;
        public CouponsController(IMapper mapper, ICouponInterface couponInterface)
        {
            _couponInterface = couponInterface;
            _mapper = mapper;
            _responseDto = new ResponseDto();

        }
        //add coupon
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<string>> AddCoupon(CouponRequestDto newCoupon)
        {
            var coupon = _mapper.Map<Coupon>(newCoupon);
            var res = await _couponInterface.AddCouponAsync(coupon);
            if (string.IsNullOrWhiteSpace(res))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occured";
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);

        }
        //get coupons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetAllCoupons()
        {
            var Coupons = await _couponInterface.GetCouponsAsync();
            if (Coupons == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occured";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = Coupons;
            return Ok(_responseDto);
        }
        [HttpGet("GetByName{code}")]
        public async Task<ActionResult<ResponseDto>> GetCoupon(string code)
        {
            var coupon = await _couponInterface.GetCouponByNameAsync(code);
            if (coupon == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occured";
                return BadRequest(_responseDto);
            }

            _responseDto.Result = coupon;
            return Ok(_responseDto);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> UpdateCoupon(Guid id, CouponRequestDto couponRequestDto)
        {
            var coupon = await _couponInterface.GetCouponByIdAsync(id);
            if (coupon == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occured";
                return BadRequest(_responseDto);
            }
            //update
            var updated = _mapper.Map(couponRequestDto, coupon);
            var response = await _couponInterface.UpdateCouponAsync(updated);
            _responseDto.Result = response;
            return Ok(_responseDto);
        }
        [HttpDelete]
       [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> DeleteCoupon(Guid id)
        {
            var coupon = await _couponInterface.GetCouponByIdAsync(id);
            if (coupon == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occured";
                return BadRequest(_responseDto);
            }
            //delete

            var response = await _couponInterface.DeleteCouponAsync(coupon);
            _responseDto.Result = response;
            return Ok(_responseDto);
        }


    }
}

using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheJituEcommerce_Auth.Models.DTOs;
using TheJituEcommerce_Auth.Services.IService;

namespace TheJituEcommerce_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserInterface _userInterface;
        private readonly ResponseDto _response;
        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
            //Don't inject just initialize
            _response = new ResponseDto();
        }
        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto>> AddUser(RegisterRequestDto registerRequestDto)
        {
            var errorMessage = await _userInterface.RegisterUser(registerRequestDto);
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;

                return BadRequest(_response);
            }
            return Ok(_response);
        }
        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> LoginUser(LoginRequestDto loginRequestDto)
        {
            var response = await _userInterface.Login(loginRequestDto);
            if(response.User == null)
            {
                //error
                _response.IsSuccess = false;
                _response.Message = "Invalid Credentials";
                return BadRequest(_response);
            }
            _response.Result = response;
            return Ok(_response);
        }
        [HttpPost("AssignRole")]
        ///assign user role
        public async Task<ActionResult<ResponseDto>> AssignRole(RegisterRequestDto registerRequestDto)
        {
            var response = await _userInterface.AssignUserRole(registerRequestDto.Email, registerRequestDto.Role);
            if (!response)
            {
                //error
                _response.IsSuccess = false;
                _response.Message = "Error Occured";

                return BadRequest(_response);
            }
            _response.Result = response;
            return Ok(_response);
        }
    }
}

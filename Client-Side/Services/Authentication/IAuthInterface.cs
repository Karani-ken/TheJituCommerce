using Client_Side.Models.Auth;
using Client_Side.Models;
namespace Client_Side.Services.Authentication
{
    public interface IAuthInterface
    {
        Task<ResponseDto> Register(RegisterRequestDto registerRequestDto);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}

using TheJituEcommerce_Auth.Models.DTOs;

namespace TheJituEcommerce_Auth.Services.IService
{
    public interface IUserInterface
    {
        Task<string> RegisterUser(RegisterRequestDto registerRequestDto);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<bool> AssignUserRole(string email, string Rolename);
    }
}

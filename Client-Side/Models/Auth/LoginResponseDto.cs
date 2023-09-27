namespace Client_Side.Models.Auth
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; } = default!;

        public string Token { get; set; } = string.Empty;
    }
}

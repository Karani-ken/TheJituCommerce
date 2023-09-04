using System.ComponentModel.DataAnnotations;

namespace TheJituEcommerce_Auth.Models.DTOs
{
    public class LoginRequestDto
    {
        [Required]

        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}

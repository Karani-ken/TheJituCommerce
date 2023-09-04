﻿using System.ComponentModel.DataAnnotations;

namespace TheJituEcommerce_Auth.Models.DTOs
{
    public class RegisterRequestDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]

        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        //optional
        public string? Role { get; set; } = string.Empty;
    }
}

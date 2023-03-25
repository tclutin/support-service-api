using System.ComponentModel.DataAnnotations;

namespace SupportService.Api.src.Controllers.dto
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string TelegramId { get; set; }
    }

    public class AuthEmployerDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RegEmployerDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

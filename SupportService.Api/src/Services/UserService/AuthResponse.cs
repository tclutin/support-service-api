using SupportService.Api.src.Entities;
using System.Text.Json.Serialization;

namespace SupportService.Api.src.Services.UserService
{
    public class AuthResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public int Rating { get; set; }
        public Guid? TicketId { get; set; }
        public Tokens Tokens { get; set; }
    }
}

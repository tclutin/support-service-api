using SupportService.Api.src.Controllers.dto;
using SupportService.Api.src.Entities;

namespace SupportService.Api.src.Services.UserService
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<User?> GetUserAsync(AuthEmployerDto dto);
        Task<User?> GetUserAsync(RegEmployerDto dto);
        Task<User?> GetUserByIdAsync(Guid? id);
        Task<User?> GetUserByTelegramIdAsync(string TelegramId);
        Task UpdateUserAsync(User user);
    }
}

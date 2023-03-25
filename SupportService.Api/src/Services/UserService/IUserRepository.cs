using SupportService.Api.src.Controllers.dto;
using SupportService.Api.src.Entities;

namespace SupportService.Api.src.Services.UserService
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);

        //2 стрёмных метода с пробросом DTO до repo / переделать
        Task<User?> GetUserAsync(AuthEmployerDto dto);
        Task<User?> GetUserAsync(RegEmployerDto dto);
        //2 стрёмных метода с пробросом DTO до repo / переделать

        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetUserByTelegramIdAsync(string TelegramId);
        Task UpdateUserAsync(User user);
    }
}

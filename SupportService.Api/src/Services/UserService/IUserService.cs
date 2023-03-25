using SupportService.Api.src.Controllers.dto;
using SupportService.Api.src.Entities;

namespace SupportService.Api.src.Services.UserService
{
    public interface IUserService
    {
        Task CreateUserAsync(UserDto dto);
        Task RegisterEmployerAsync(RegEmployerDto dto);
        Task<AuthResponse> LoginEmployerAsync(AuthEmployerDto dto);

        Task<User> GetUserByTelegramId(string id);
    }
}

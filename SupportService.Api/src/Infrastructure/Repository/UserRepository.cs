
using Microsoft.EntityFrameworkCore;
using SupportService.Api.src.Controllers.dto;
using SupportService.Api.src.Entities;
using SupportService.Api.src.Infrastructure.Repository;
using SupportService.Api.src.Services.UserService;

namespace SupportService.Api.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    { 
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext userDbContext)
        {
            _applicationDbContext = userDbContext;
        }

        public async Task CreateUserAsync(User user)
        {
            await _applicationDbContext.Users.AddAsync(user);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserAsync(AuthEmployerDto dto)
        {
            return await _applicationDbContext.Users
                .Where(u => u.Username == dto.Username && u.Password == dto.Password)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserAsync(RegEmployerDto dto)
        {
            return await _applicationDbContext.Users
                .Where(u => u.Username == dto.Username && u.Password == dto.Password)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByTelegramIdAsync(string TelegramId)
        {
            return await _applicationDbContext.Users
                .Where(u => u.TelegramId == TelegramId)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _applicationDbContext.Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _applicationDbContext.Users.Update(user);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}

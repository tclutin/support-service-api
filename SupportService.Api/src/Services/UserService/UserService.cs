using Microsoft.IdentityModel.Tokens;
using SupportService.Api.src.Controllers.dto;
using SupportService.Api.src.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tweetinvi.Security;

namespace SupportService.Api.src.Services.UserService
{
    public class UserService : IUserService
    {

        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task RegisterEmployerAsync(RegEmployerDto dto)
        {
            if (dto == null)
            {
                throw new Exception("Invalid request");
            }

            var user = await _userRepository.GetUserAsync(dto);
            if (user != null)
            {
                throw new Exception("The user already exists");
            }

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email,
                Role = "worker",
                Rating = 0

            };

            await _userRepository.CreateUserAsync(newUser);
        }

        public async Task<AuthResponse> LoginEmployerAsync(AuthEmployerDto dto)
        {
            if (dto == null)
            {
                throw new Exception("Invalid request");
            }

            var user = await _userRepository.GetUserAsync(dto);
            if (user == null)
            {
                throw new Exception("There is no such user");
            }

            var authResponse = new AuthResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Rating = user.Rating,
                Role= user.Role,
                TicketId = user.TicketId,
                Tokens = new Tokens { AccessToken = GenerateToken(user.Username) }
            };

            return authResponse;
        }

        public async Task CreateUserAsync(UserDto dto)
        {
            if (dto == null)
            {
                throw new Exception("Invalid request");
            }

            var user = await _userRepository.GetUserByTelegramIdAsync(dto.TelegramId);
            if (user!= null)
            {
                throw new Exception("User already exist");
            }

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Username = dto.Name,
                TelegramId = dto.TelegramId,
                Role = "user",
            };

            await _userRepository.CreateUserAsync(newUser);
        }

        public async Task<User> GetUserByTelegramId(string id)
        {
            var user = await _userRepository.GetUserByTelegramIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.Role != "user")
            {
                throw new Exception("You cant get a worker");
            }

            return user;
        }

        private string GenerateToken(string username)
        {
            var singingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
            };

            var securityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: claims,
                signingCredentials: singingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

    }
}

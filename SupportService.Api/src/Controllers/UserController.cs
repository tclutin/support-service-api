
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportService.Api.src.Controllers.dto;
using SupportService.Api.src.Services.UserService;
using SupportService.Api.src.Utilities;

namespace SupportService.Api.src.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService UserService)
        {
            _userService = UserService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(UserDto dto)
        {
            try
            {
                await _userService.CreateUserAsync(dto);
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = new { message = "User successfully created" }
                });
            }
            catch (Exception ex)
            {
                return ex.ToApiErrorResponse();
            }
        }

        [HttpGet("{telegramId}/get")]
        public async Task<IActionResult> GetUserByTelegramId(string telegramId)
        {
            try
            {
                var user = await _userService.GetUserByTelegramId(telegramId);
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = user
                });
            }
            catch (Exception ex)
            {
                return ex.ToApiErrorResponse();
            }
        }
    }
}
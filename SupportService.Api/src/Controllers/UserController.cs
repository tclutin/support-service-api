
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportService.Api.src.Controllers.dto;
using SupportService.Api.src.Services.UserService;

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

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(UserDto dto)
        {
            try
            {
                await _userService.CreateUserAsync(dto);
                return Ok(new { message = "User successfully created" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{telegramId}/get")]
        public async Task<IActionResult> GetUserByTelegramId(string telegramId)
        {
            try
            {
                var user = await _userService.GetUserByTelegramId(telegramId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
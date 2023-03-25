
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportService.Api.src.Controllers.dto;

using SupportService.Api.src.Services.UserService;

namespace SupportService.Api.src.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService UserService)
        {
            _userService = UserService;
        }

        [Authorize]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(RegEmployerDto dto)
        {
            try
            {
                await _userService.RegisterEmployerAsync(dto);
                return Ok(new { message = "Employer created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(AuthEmployerDto dto)
        {
            try
            {
                var user = await _userService.LoginEmployerAsync(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

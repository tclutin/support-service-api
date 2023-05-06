
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportService.Api.src.Controllers.dto;

using SupportService.Api.src.Services.UserService;
using SupportService.Api.src.Utilities;

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

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(RegEmployerDto dto)
        {
            try
            {   
                await _userService.RegisterEmployerAsync(dto);
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = new { message = "You have successfully registered" }
                });
            }
            catch (Exception ex)
            {
                return ex.ToApiErrorResponse();
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LogIn(AuthEmployerDto dto)
        {
            try
            {
                var user = await _userService.LoginEmployerAsync(dto);
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

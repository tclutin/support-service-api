using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using SupportService.Api.src.Services.UserService;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace SupportService.Api.src.Controllers.Middleware
{
    //Проблема: если хоть что то указано в Authorization, то выдаст 401, если это поле не будет указано, то запрос вполне пройдёт, если используется AllowAnonymous / не работает
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //soon
        }

    }
}

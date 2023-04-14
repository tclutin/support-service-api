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
            if (context.Request.Headers.TryGetValue("Authorization", out var authHeader) && authHeader.ToString().StartsWith("Bearer "))
            {
                var token = authHeader.ToString().Substring("Bearer ".Length);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "idk",
                    ValidAudience = "idk",
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("aPdSgUkXp2s5v8y/"))
                };

                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var user = handler.ValidateToken(token, validationParameters, out var validatedToken);

                    context.Items["jwt"] = token;
                }
                catch (Exception)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
            }

            await _next(context);
        }


    }
}

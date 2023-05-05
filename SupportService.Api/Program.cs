
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SupportService.Api.Infrastructure.Repository;
using SupportService.Api.src.Common;
using SupportService.Api.src.Controllers.Middleware;
using SupportService.Api.src.Infrastructure.Repository;
using SupportService.Api.src.Services.TelegramService;
using SupportService.Api.src.Services.TicketService;
using SupportService.Api.src.Services.UserService;
using System.Text;

namespace SupportService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                IConfiguration configuration = builder.Configuration;

                builder.Services.AddDbContext<ApplicationDbContext>(opt =>
                    opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SupportService.Api")));

                builder.Services.AddScoped<IUserService, UserService>();
                builder.Services.AddScoped<IUserRepository, UserRepository>();

                builder.Services.AddScoped<ITicketService, TicketService>();
                builder.Services.AddScoped<ITicketRepository, TicketRepository>();

                builder.Services.AddScoped<ITelegramService, TelegramService>();

                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                .AddJwtBearer(options =>
                                {
                                    options.TokenValidationParameters = new TokenValidationParameters
                                    {
                                        ValidateIssuer = true,
                                        ValidIssuer = configuration["JWT:Issuer"],

                                        ValidateAudience = true,
                                        ValidAudience = configuration["JWT:Audience"],

                                        ValidateLifetime = true,
                                        LifetimeValidator = JwtHelper.CustomLifetimeValidator,
                                        RequireExpirationTime = true,

                                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                                        ValidateIssuerSigningKey = true,
                                    };
                                });

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
            }

            var app = builder.Build();
            {
                if (app.Environment.IsDevelopment())
                {   
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                
                app.UseCors(builder => builder.AllowAnyOrigin());

                app.UseHttpsRedirection();

                app.MapControllers();

                app.Run();
            }

        }
    }
}
    

using System.Net;

namespace SupportService.Api.src.Services.TelegramService
{
    public class TelegramService : ITelegramService
    {
        private readonly IConfiguration _configuration;

        public TelegramService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMessage(string chat_id, string message)
        {
            string url = "https://api.telegram.org/bot" + _configuration["tgtoken"] + "/sendMessage?" + "chat_id=" + chat_id + "&text=" + message;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
            }
        }
    }
}

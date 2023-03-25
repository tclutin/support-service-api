namespace SupportService.Api.src.Services.TelegramService
{
    public interface ITelegramService
    {
        Task SendMessage(string chat_id, string message);
    }
}

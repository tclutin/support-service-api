using SupportService.Api.src.Controllers.dto;
using SupportService.Api.src.Entities;

namespace SupportService.Api.src.Services.TicketService
{
    public interface ITicketService
    {
        Task CreateTicketAsync(TicketDto dto);
        Task SendMessageAsync(Guid ticketId, MessageDto dto);
        Task AssignToUserAsync(Guid ticketId, AssignTicketDto dto);
        Task<IEnumerable<Ticket>> GetAllOpenTicketsAsync();
        Task CloseTicket(Guid ticketId);
    }
}

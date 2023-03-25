using SupportService.Api.src.Controllers.dto;
using SupportService.Api.src.Entities;

namespace SupportService.Api.src.Services.TicketService
{
    public interface ITicketRepository
    {
        Task CreateTicketAsync(Ticket ticket);
        Task CreateMessageAsync(Message message);
        Task<Ticket?> GetTicketByIdAsync(Guid Id);
        Task<IEnumerable<Ticket>> GetAllOpenTicketsAsync();
        Task UpdateTicketAsync(Ticket ticket);
    }
}

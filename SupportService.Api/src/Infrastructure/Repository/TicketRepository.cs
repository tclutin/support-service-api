using Microsoft.EntityFrameworkCore;
using SupportService.Api.src.Entities;
using SupportService.Api.src.Services.TicketService;


namespace SupportService.Api.src.Infrastructure.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TicketRepository(ApplicationDbContext userDbContext)
        {
            _applicationDbContext = userDbContext;
        }

        public async Task CreateTicketAsync(Ticket ticket)
        {
            await _applicationDbContext.Tickets.AddAsync(ticket);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Ticket?> GetTicketByIdAsync(Guid id)
        {
            return await _applicationDbContext.Tickets
                .Include(t => t.Messages)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task CreateMessageAsync(Message message)
        {
            await _applicationDbContext.Messages.AddAsync(message);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _applicationDbContext.Tickets.Update(ticket);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Ticket>> GetAllOpenTicketsAsync()
        {
            return await _applicationDbContext.Tickets
                .Where(t => t.Status == "open")
                .ToListAsync();
        }
    }
}

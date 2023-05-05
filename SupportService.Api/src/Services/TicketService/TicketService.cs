using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SupportService.Api.src.Controllers.dto;
using SupportService.Api.src.Entities;
using SupportService.Api.src.Services.TelegramService;
using SupportService.Api.src.Services.UserService;

namespace SupportService.Api.src.Services.TicketService
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITelegramService _telegramService;

        public TicketService(IUserRepository userRepository, ITicketRepository ticketRepository, ITelegramService telegramService)
        {
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
            _telegramService = telegramService;
        }

        public async Task CreateTicketAsync(TicketDto dto)
        {
            if (dto == null)
            {
                throw new Exception("Invalid request");
            }

            var user = await _userRepository.GetUserByIdAsync(dto.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.Role == "worker")
            {
                throw new Exception("You cannot create tickets");
            }

            if (user.TicketId != null)
            {
                throw new Exception("You already have an active ticket");
            }

            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                CreatedByUserId = dto.UserId,
                CreatedDate = DateTime.UtcNow,
                Status = "open",
                Messages = new List<Message>()
            };

            user.TicketId = ticket.Id;

            await _userRepository.UpdateUserAsync(user);

            await _ticketRepository.CreateTicketAsync(ticket);
        }

        public async Task SendMessageAsync(Guid ticketId, MessageDto dto)
        {
            if (dto == null)
            {
                throw new Exception("Invalid request");
            }

            var sender = await _userRepository.GetUserByIdAsync(dto.SenderId);
            if (sender == null)
            {
                throw new Exception("Sender not found");
            }

            var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            if (ticket.Status != "inprogress")
            {
                throw new Exception("The ticket has the open or close status");
            }

            if (ticket.AssignedToUserId != dto.SenderId && ticket.CreatedByUserId != dto.SenderId)
            {
                throw new Exception("You are not a party to Ticket");
            }

            var message = new Message
            {
                Id = Guid.NewGuid(),
                TicketId = ticketId,
                SenderId = dto.SenderId,
                CreatedAt = DateTime.UtcNow,
                Text = dto.Text,
                Sender = sender,
                Ticket = ticket
            };

            if (sender.Role == "worker")
            {
              await _telegramService.SendMessage(sender.TelegramId, dto.Text);
            }

            await _ticketRepository.CreateMessageAsync(message);
        }

        public async Task<Ticket> GetTicketByIdAsync(Guid ticketid)
        {
           var ticket = await _ticketRepository.GetTicketByIdAsync(ticketid);

            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetAllOpenTicketsAsync()
        {
            return await _ticketRepository.GetAllOpenTicketsAsync();
        }

        public async Task AssignToUserAsync(Guid ticketId, AssignTicketDto dto)
        {
            if (dto == null)
            {
                throw new Exception("Invalid request");
            }

            var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            var user = await _userRepository.GetUserByIdAsync(dto.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (ticket.AssignedToUserId != null)
            {
                throw new Exception("The ticket has already been assigned");
            }

            if (user.Role != "worker")
            {
                throw new Exception("You are not an employer");
            }

            var createdByUserId = await _userRepository.GetUserByIdAsync(ticket.CreatedByUserId);

            user.TelegramId = createdByUserId.TelegramId;
            user.TicketId = ticketId;

            ticket.Status = "inprogress";
            ticket.AssignedToUserId = dto.UserId;

            await _userRepository.UpdateUserAsync(user);
            await _ticketRepository.UpdateTicketAsync(ticket);

            await _telegramService.SendMessage(createdByUserId.TelegramId, $"[System] Вам назначен сотрудник {user.Username} с рейтингом {user.Rating}");
        }

        //добавить закрывтие тикета и очистку полей у пользователей
        public async Task CloseTicket(Guid ticketId)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
            if (ticket == null)
                throw new Exception("Ticket not found");

            ticket.Status = "close";

            await _ticketRepository.UpdateTicketAsync(ticket);
        }
    }
}

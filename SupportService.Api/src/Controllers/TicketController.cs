using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportService.Api.src.Controllers.dto;
using SupportService.Api.src.Entities;
using SupportService.Api.src.Services.TicketService;
using SupportService.Api.src.Services.UserService;
using SupportService.Api.src.Utilities;

namespace SupportService.Api.src.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateTicket(TicketDto dto)
        {
            try
            {
                await _ticketService.CreateTicketAsync(dto);
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = new { message = "Ticket successfully created" }
                });
            }
            catch (Exception ex)
            {
                return ex.ToApiErrorResponse();
            }
        }

        [Authorize]
        [HttpGet("all-open-tickets")]
        public async Task<IActionResult> GetAllTickets()
        {
            try
            {
                var tickets = await _ticketService.GetAllOpenTicketsAsync();
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = tickets
                });
            }
            catch (Exception ex)
            {
                return ex.ToApiErrorResponse();
            }
        }

        [Authorize]
        [HttpPost("{ticketId}/messages")]
        public async Task<IActionResult> SendTicketMessage(Guid ticketId, [FromBody] MessageDto dto)
        {
            try
            {
                await _ticketService.SendMessageAsync(ticketId, dto);
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = new { message = "The message was sent successfully" }
                });
            }
            catch (Exception ex)
            {
                return ex.ToApiErrorResponse();
            }
        }

        [Authorize]
        [HttpPut("{ticketId}/assign")]
        public async Task<IActionResult> AssignToUser(Guid ticketId, [FromBody] AssignTicketDto dto)
        {
            try
            {
                await _ticketService.AssignToUserAsync(ticketId, dto);
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = new { message = "User have been successfully assigned" }
                });
            }
            catch (Exception ex)
            {
                return ex.ToApiErrorResponse();
            }
        }

        [Authorize]
        [HttpPut("{ticketId}/close")]
        public async Task<IActionResult> CloseTicket(Guid ticketId)
        {
            try
            {
                await _ticketService.CloseTicket(ticketId);
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = new { message = "The ticket is closed" }
                });
            }
            catch (Exception ex)
            {
                return ex.ToApiErrorResponse();
            }
        }
    }
}

/*
      [HttpPut("{ticketId}/status")]
public async Task<IActionResult> UpdateTicketStatus(Guid ticketId, [FromBody] TicketStatusDto dto)
{
    try
    {
        await _ticketService.UpdateTicketStatusAsync(ticketId, dto);
        return Ok(new { message = "Ticket status updated" });
    }
    catch (Exception ex)
    {
        return BadRequest(new { message = ex.Message });
    }
}

*/
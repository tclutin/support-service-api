using System.ComponentModel.DataAnnotations;

namespace SupportService.Api.src.Controllers.dto
{
    public class TicketDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }

    public class TicketStatusDto
    {
        [Required]
        public string Status { get; set; }
    }

    public class AssignTicketDto
    {
        [Required]
        public Guid UserId { get; set; }
    }
}

using SupportService.Api.src.Entities;
using System.ComponentModel.DataAnnotations;

namespace SupportService.Api.src.Controllers.dto
{
    public class MessageDto
    {
        [Required]
        public Guid SenderId { get; set; }

        [Required]
        public string Text { get; set; }
    }
}

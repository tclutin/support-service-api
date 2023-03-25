namespace SupportService.Api.src.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public Guid SenderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public User Sender { get; set; }
        public Ticket Ticket { get; set; }
    }
}
        
namespace SupportService.Api.src.Entities
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid? AssignedToUserId { get; set; }
        public string Status { get; set; }
        public ICollection<Message> Messages { get; set; }
    }

}

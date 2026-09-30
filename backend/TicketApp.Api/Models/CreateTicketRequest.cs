namespace TicketApp.Api.Models
{
    public class CreateTicketRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public TicketPriority Priority { get; set; }
        public DateOnly? DueDate { get; set; }
        public int ProjectId { get; set; }
    }
}
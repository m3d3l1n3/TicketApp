namespace TicketApp.Api.Models
{
    public class CreateTicketRequest
    {
        public required String Title { get; set; }
        public required String Description { get; set; }
        public TicketPriority Priority { get; set; }
        public DateOnly? DueDate { get; set; }
        public int ProjectId { get; set; }
    }
}
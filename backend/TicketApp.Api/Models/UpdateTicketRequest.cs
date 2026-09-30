namespace TicketApp.Api.Models
{
    public class UpdateTicketRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateOnly? DueDate { get; set; }
        public int ProjectId { get; set; }
    }
}
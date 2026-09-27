namespace TicketApp.Api.Controllers
{
    public class UpdateTicketRequest
    {
        public required String Title { get; set; }
        public required String Description { get; set; }
        public DateOnly? DueDate { get; set; }
        public int ProjectId { get; set; }
    }
}
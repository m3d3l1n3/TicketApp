namespace TicketApp.Api.Models
{
    public class CreateProjectRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

    }
}
namespace TicketApp.Api.Models
{
    public class Project
    {
        public string? Description { get; set; }
        public int Id { get; }
        public string Name { get; private set; }
        public List<Ticket> Tickets { get; } = [];
        public Project(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

    }
}
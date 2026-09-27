namespace TicketApp.Api.Models
{
    public class Project
    {
        public string? Description { get; set; }
        public int Id { get; }
        public string Name { get; private set; }
        public List<Ticket> Tickets { get; } = [];
        public Project(int id, string name, string? description)
        {
            this.Id = id;

            if (!string.IsNullOrWhiteSpace(name))
                this.Name = name;
            else throw new ArgumentException("Project name cannot be empty.");

            Description = description;
        }

    }
}
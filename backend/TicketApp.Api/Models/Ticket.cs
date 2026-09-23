namespace TicketApp.Api.Models
{
    public class Ticket
    {
        public DateTime CreatedAt { get; private set; }
        public DateOnly? DueDate { get; set; }
        public string Title { get; }
        public string Description { get; private set; }
        public TicketStatus Status { get; private set; }
        public TicketPriority Priority { get; set; }
        public int ProjectId { get; set; }
        public int Id { get; }


        public Ticket(int id, string title, string description)
        {
            if (!string.IsNullOrWhiteSpace(title))// if (title != string.Empty && title != null && title != " ")
                this.Title = title;
            else throw new ArgumentException("Ticket title cannot be empty");
            this.Id = id;
            this.Description = description;
            this.Status = TicketStatus.Open;
            this.CreatedAt = DateTime.UtcNow;
        }

        public void StartProgress()
        {
            if (Status == TicketStatus.Open)
                Status = TicketStatus.InProgress;
            else throw new InvalidOperationException("Cannot set ticket to in progress status. Only Open tickets can be set to in progress.");
        }
        public void Resolve()
        {
            if (Status == TicketStatus.InProgress)
                Status = TicketStatus.Resolved;
            else throw new InvalidOperationException("Cannot set ticket to resolved status. Only InProgress tickets can be set to resolved.");

        }
        public void Close()
        {
            if (Status == TicketStatus.Resolved)
                Status = TicketStatus.Closed;
            else throw new InvalidOperationException("Cannot set ticket to closed status. Only Resolved tickets can be set to closed.");

        }
        public void Reopen()
        {
            if (Status == TicketStatus.Closed)
                Status = TicketStatus.Open;
            else throw new InvalidOperationException("Cannot set ticket to open status. Only closed tickets can be set to (re)open.");

        }
    }
}
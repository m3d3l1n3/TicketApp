namespace TicketApp.Api.Models
{
    public class TicketStore
    {
        private readonly List<Ticket> tickets = [];
        public void AddTicket(Ticket ticket)
        {
            tickets.Add(ticket);
        }
        public Ticket GetTicket(int id)
        {
            var res = tickets.FirstOrDefault(t => t.Id == id);
            if (res is not null)
                return res;
            else throw new ArgumentException("Ticket id does not exist.");
        }
        public List<Ticket> GetTickets()
        {
            return tickets.ToList();
        }
        public List<Ticket> GetTicketsWithStatus(TicketStatus status)
        {
            return tickets.Where(t => t.Status == status).ToList();
        }
        public List<Ticket> GetTicketsWithPriority(TicketPriority priority)
        {
            return tickets.Where(t => t.Priority == priority).OrderBy(t => !t.DueDate.HasValue).ThenBy(t => t.DueDate).ToList();

        }
        public List<Ticket> GetTicketsOverdue()
        {
            return tickets.Where(t => t.DueDate < DateOnly.FromDateTime(DateTime.Today) && t.Status != TicketStatus.Closed).ToList();
        }
        public Dictionary<TicketStatus, int> GetTicketCountsByStatus()
        {
            return tickets.GroupBy(t => t.Status).ToDictionary(g => g.Key, g => g.Count());
        }
        public List<Ticket> GetTicketsByProject(int projectId)
        {
            return tickets.Where(t => t.ProjectId == projectId).ToList();
        }
    }
}
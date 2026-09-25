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

        //Part A
        public List<Ticket> GetCurrentTickets()
        {
            return tickets.Where(t => t.Status != TicketStatus.Resolved && t.Status != TicketStatus.Closed).OrderByDescending(t => t.Priority).ThenBy(t => !t.DueDate.HasValue).ThenBy(t => t.DueDate).ToList();
        }
        public ProjectReport GetProjectReport(int projectId)
        {

            int total = tickets.Count(t => t.ProjectId == projectId); //total
            int open = tickets.Count(t => t.ProjectId == projectId && t.Status != TicketStatus.Resolved && t.Status != TicketStatus.Closed); //open tickets
            int overdue = tickets.Count(t => t.ProjectId == projectId && t.DueDate < DateOnly.FromDateTime(DateTime.Today) && t.Status != TicketStatus.Closed); //overdue tickets

            var ticket = tickets.Where(t => t.ProjectId == projectId && t.DueDate >= DateOnly.FromDateTime(DateTime.Today)).OrderBy(t => !t.DueDate.HasValue).ThenBy(t => t.DueDate).FirstOrDefault();
            ProjectReport report = new(projectId, total, open, overdue, ticket?.DueDate);

            return report;
        }
        public List<ProjectReport> GetAllProjectReports()
        {
            return tickets.GroupBy(t => t.ProjectId).Select(t => new ProjectReport(
                t.Key,
                t.Count(),
                t.Count(t => t.Status != TicketStatus.Resolved && t.Status != TicketStatus.Closed),
                t.Count(t => t.DueDate < DateOnly.FromDateTime(DateTime.Today) && t.Status != TicketStatus.Closed),
                t.Where(g => g.DueDate.HasValue && g.DueDate >= DateOnly.FromDateTime(DateTime.Today)).OrderBy(g => g.DueDate).FirstOrDefault()?.DueDate)).ToList();
        }
    }
}
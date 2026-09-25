namespace TicketApp.Api.Models
{
    public class ProjectReport
    {
        public int ProjectId { get; private set; }
        public int TotalTickets { get; private set; }
        public int OpenTickets { get; private set; }
        public int OverdueTickets { get; private set; }
        public DateOnly? EarliestDueDate { get; private set; }

        public ProjectReport(int projectId, int totalTickets, int openTickets, int overdueTickets, DateOnly? earliestDueDate)
        {
            ProjectId = projectId;
            TotalTickets = totalTickets;
            OpenTickets = openTickets;
            OverdueTickets = overdueTickets;
            EarliestDueDate = earliestDueDate;
        }
    }
}
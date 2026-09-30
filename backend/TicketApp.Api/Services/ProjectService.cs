using TicketApp.Api.Models;
using TicketApp.Api.Data;
using Microsoft.EntityFrameworkCore;
namespace TicketApp.Api.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext context;
    public ProjectService(AppDbContext context) { this.context = context; }
    public Project GetProject(int id)
    {
        var project = context.Projects.AsNoTracking().FirstOrDefault(p => p.Id == id);
        if (project is null)
            throw new ArgumentException($"Project with {id} does not exist.");
        return project;
    }
    public List<Project> GetProjects()
    {
        return context.Projects.AsNoTracking().OrderBy(p => p.Name).ToList();

    }
    public ProjectReport GetProjectReport(int id)
    {
        if (context.Projects.FirstOrDefault(p => p.Id == id) is null)
            throw new ArgumentException($"Project with {id} does not exist");

        int total = context.Tickets.Count(t => t.ProjectId == id);
        int open = context.Tickets.Count(t => t.ProjectId == id && t.Status != TicketStatus.Resolved && t.Status != TicketStatus.Closed);
        int overdue = context.Tickets.Count(t => t.ProjectId == id && t.DueDate < DateOnly.FromDateTime(DateTime.Today) && t.Status != TicketStatus.Closed);
        var ticket = context.Tickets.Where(t => t.ProjectId == id && t.DueDate >= DateOnly.FromDateTime(DateTime.Today)).OrderBy(t => !t.DueDate.HasValue).ThenBy(t => t.DueDate).FirstOrDefault();
        ProjectReport report = new(id, total, open, overdue, ticket?.DueDate);
        return report;
    }
    public Project CreateProject(CreateProjectRequest request)
    {

        Project project = new(request.Name, request.Description);
        context.Projects.Add(project);
        context.SaveChanges();
        return project;

    }
}
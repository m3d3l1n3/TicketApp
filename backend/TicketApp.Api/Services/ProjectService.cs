using TicketApp.Api.Models;
using TicketApp.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace TicketApp.Api.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext context;
    public ProjectService(AppDbContext context) { this.context = context; }
    public async Task<Project> GetProject(int id)
    {
        var project = await context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (project is null)
            throw new ArgumentException($"Project with {id} does not exist.");
        return project;
    }
    public async Task<List<Project>> GetProjects()
    {
        return await context.Projects.AsNoTracking().OrderBy(p => p.Name).ToListAsync();

    }
    public async Task<ProjectReport> GetProjectReport(int id)
    {
        if (await context.Projects.FirstOrDefaultAsync(p => p.Id == id) is null)
            throw new ArgumentException($"Project with {id} does not exist");

        var total = await context.Tickets.CountAsync(t => t.ProjectId == id);
        var open = await context.Tickets.CountAsync(t => t.ProjectId == id && t.Status != TicketStatus.Resolved && t.Status != TicketStatus.Closed);
        var overdue = await context.Tickets.CountAsync(t => t.ProjectId == id && t.DueDate < DateOnly.FromDateTime(DateTime.Today) && t.Status != TicketStatus.Closed);
        var ticket = await context.Tickets.Where(t => t.ProjectId == id && t.DueDate >= DateOnly.FromDateTime(DateTime.Today)).OrderBy(t => !t.DueDate.HasValue).ThenBy(t => t.DueDate).FirstOrDefaultAsync();
        ProjectReport report = new(id, total, open, overdue, ticket?.DueDate);
        return report;
    }
    public async Task<Project> CreateProject(CreateProjectRequest request)
    {

        Project project = new(request.Name, request.Description);
        context.Projects.Add(project);
        await context.SaveChangesAsync();
        return project;
    }
}
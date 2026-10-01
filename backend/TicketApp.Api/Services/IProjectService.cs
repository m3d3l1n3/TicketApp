using TicketApp.Api.Models;
using TicketApp.Api.Data;
namespace TicketApp.Api.Services;

public interface IProjectService
{
    Task<Project> GetProject(int id);
    Task<List<Project>> GetProjects();
    Task<ProjectReport> GetProjectReport(int id);
    Task<Project> CreateProject(CreateProjectRequest request);

}
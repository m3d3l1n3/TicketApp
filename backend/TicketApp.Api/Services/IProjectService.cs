using TicketApp.Api.Models;
using TicketApp.Api.Data;
namespace TicketApp.Api.Services;

public interface IProjectService
{
    Project GetProject(int id);
    List<Project> GetProjects();
    ProjectReport GetProjectReport(int id);
    Project CreateProject(CreateProjectRequest request);

}
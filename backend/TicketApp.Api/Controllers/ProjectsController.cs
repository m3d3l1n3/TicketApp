using Microsoft.AspNetCore.Mvc;
using TicketApp.Api.Data;
using TicketApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace TicketApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext context;
        public ProjectsController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet("{id}")]
        public ActionResult<Project> GetProject(int id)
        {
            var project = context.Projects.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (project is null)
                return NotFound();
            return Ok(project);
        }
        [HttpGet]
        public ActionResult<List<Project>> GetProjects()
        {
            var projects = context.Projects.AsNoTracking().OrderBy(p => p.Name).ToList();
            return Ok(projects);
        }
        [HttpGet("{id}/report")]
        public ActionResult<ProjectReport> GetProjectReport(int id)
        {

            if (context.Projects.FirstOrDefault(p => p.Id == id) is null)
                return NotFound("Project does not exist");

            int total = context.Tickets.Count(t => t.ProjectId == id);
            int open = context.Tickets.Count(t => t.ProjectId == id && t.Status != TicketStatus.Resolved && t.Status != TicketStatus.Closed);
            int overdue = context.Tickets.Count(t => t.ProjectId == id && t.DueDate < DateOnly.FromDateTime(DateTime.Today) && t.Status != TicketStatus.Closed);
            var ticket = context.Tickets.Where(t => t.ProjectId == id && t.DueDate >= DateOnly.FromDateTime(DateTime.Today)).OrderBy(t => !t.DueDate.HasValue).ThenBy(t => t.DueDate).FirstOrDefault();
            ProjectReport report = new(id, total, open, overdue, ticket?.DueDate);
            return Ok(report);

        }
        [HttpPost]
        public ActionResult<Project> CreateProject([FromBody] CreateProjectRequest request)
        {
            try
            {
                Project project = new(request.Name, request.Description);
                context.Projects.Add(project);
                context.SaveChanges();
                return CreatedAtAction(nameof(GetProject), new { project.Id }, project);

            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
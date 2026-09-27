using Microsoft.AspNetCore.Mvc;
using TicketApp.Api.Models;
namespace TicketApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectStore store;
        private readonly TicketStore tickets;
        public ProjectsController(ProjectStore store, TicketStore tickets)
        {
            this.store = store;
            this.tickets = tickets;
        }
        [HttpGet("{id}")]
        public ActionResult<Project> GetProject(int id)
        {
            try
            {
                return Ok(store.GetProject(id));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
        [HttpGet]
        public ActionResult<List<Project>> GetProjects()
        {
            return Ok(store.GetProjects());
        }
        [HttpGet("{id}/report")]
        public ActionResult<ProjectReport> GetProjectReport(int id)
        {
            try
            {
                if (!store.ExistProject(id))
                    return NotFound("Project does not exist");
                return Ok(tickets.GetProjectReport(id));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public ActionResult<Project> CreateProject([FromBody] CreateProjectRequest request)
        {
            try
            {
                Project project = new(store.GetNextId(), request.Name, request.Description);
                store.AddProject(project);
                return CreatedAtAction(nameof(GetProject), new { project.Id }, project);

            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
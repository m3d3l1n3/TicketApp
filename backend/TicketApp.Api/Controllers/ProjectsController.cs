using Microsoft.AspNetCore.Mvc;
using TicketApp.Api.Models;
using TicketApp.Api.Services;
namespace TicketApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService projectService;
        public ProjectsController(IProjectService projectService)
        {
            this.projectService = projectService;
        }
        [HttpGet("{id}")]
        public ActionResult<Project> GetProject(int id)
        {
            try
            {
                return Ok(projectService.GetProject(id));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet]
        public ActionResult<List<Project>> GetProjects()
        {
            return Ok(projectService.GetProjects());
        }
        [HttpGet("{id}/report")]
        public ActionResult<ProjectReport> GetProjectReport(int id)
        {
            try
            {
                return Ok(projectService.GetProjectReport(id));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPost]
        public ActionResult<Project> CreateProject([FromBody] CreateProjectRequest request)
        {
            try
            {
                Project project = projectService.CreateProject(request);
                return CreatedAtAction(nameof(GetProject), new { project.Id }, project);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
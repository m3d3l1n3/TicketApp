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
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            try
            {
                return Ok(await projectService.GetProject(id));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetProjects()
        {
            return Ok(await projectService.GetProjects());
        }
        [HttpGet("{id}/report")]
        public async Task<ActionResult<ProjectReport>> GetProjectReport(int id)
        {
            try
            {
                return Ok(await projectService.GetProjectReport(id));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject([FromBody] CreateProjectRequest request)
        {
            try
            {
                var project = await projectService.CreateProject(request);
                return CreatedAtAction(nameof(GetProject), new { project.Id }, project);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
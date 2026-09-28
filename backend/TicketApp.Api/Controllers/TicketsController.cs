using Microsoft.AspNetCore.Mvc;
using TicketApp.Api.Models;
using TicketApp.Api.Data;
using Microsoft.EntityFrameworkCore;
namespace TicketApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        // private readonly TicketStore store;
        // private readonly ProjectStore projects;
        private readonly AppDbContext context;
        public TicketsController(AppDbContext context) { this.context = context; }

        [HttpGet("{id}")]
        public ActionResult<Ticket> GetTicket(int id)
        {
            var ticket = context.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == id);
            if (ticket is null)
                return NotFound();
            return Ok(ticket);


        }
        [HttpPost]
        public ActionResult<Ticket> CreateTicket([FromBody] CreateTicketRequest request)
        {
            try
            {
                if (!context.Projects.Any(p => p.Id == request.ProjectId))
                    return BadRequest("Project id is invalid.");
                var ticket = new Ticket(request.Title, request.Description, request.ProjectId, request.DueDate, request.Priority);
                context.Tickets.Add(ticket);
                context.SaveChanges();

                return CreatedAtAction(nameof(GetTicket), new { ticket.Id }, ticket);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("queue")]
        public List<Ticket> GetQueue()
        {
            return context.Tickets.AsNoTracking().Where(t => t.Status != TicketStatus.Resolved && t.Status != TicketStatus.Closed).OrderByDescending(t => t.Priority).ThenBy(t => !t.DueDate.HasValue).ThenBy(t => t.DueDate).ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<Ticket> UpdateTicket(int id, [FromBody] UpdateTicketRequest request)
        {
            var ticket = context.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket is null)
                return NotFound();
            try
            {
                ticket.Update(request.Title, request.Description, request.DueDate, request.ProjectId);
                context.SaveChanges();
                return Ok(ticket);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("{id}/start")]
        public ActionResult<Ticket> StartProgress(int id)
        {
            var ticket = context.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket is null)
                return NotFound();

            try
            {
                ticket.StartProgress();
                context.SaveChanges();
                return Ok(ticket);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
        }
        [HttpPost("{id}/resolve")]
        public ActionResult<Ticket> Resolve(int id)
        {

            var ticket = context.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket is null)
                return NotFound();
            try
            {
                ticket.Resolve();
                context.SaveChanges();
                return Ok(ticket);
            }

            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
        }
        [HttpPost("{id}/close")]
        public ActionResult<Ticket> Close(int id)
        {
            var ticket = context.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket is null) return NotFound();
            try
            {
                ticket.Close();
                context.SaveChanges();
                return Ok(ticket);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpPost("{id}/reopen")]
        public ActionResult<Ticket> Reopen(int id)
        {
            var ticket = context.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket is null) return NotFound();
            try
            {
                ticket.Reopen();
                context.SaveChanges();
                return Ok(ticket);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTicket(int id)
        {
            int deleted = context.Tickets.Where(t => t.Id == id).ExecuteDelete();
            if (deleted == 0)
                return NotFound();
            return NoContent();
        }
        // store.DeleteTicket(id) ? NoContent() : NotFound();
        [HttpGet]
        public List<Ticket> GetAll(TicketStatus? status, TicketPriority? priority, bool overdue)
        {
            IQueryable<Ticket> query = context.Tickets.AsNoTracking();
            if (status is not null) query = query.Where(t => t.Status == status);
            if (priority is not null) query = query.Where(t => t.Priority == priority);
            if (overdue) query = query.Where(t => t.DueDate < DateOnly.FromDateTime(DateTime.Today) && t.Status != TicketStatus.Closed);
            return query.ToList();
        }
    }
}
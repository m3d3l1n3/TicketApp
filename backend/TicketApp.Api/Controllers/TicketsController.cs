using Microsoft.AspNetCore.Mvc;
using TicketApp.Api.Models;
namespace TicketApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly TicketStore store;
        public TicketsController(TicketStore store)
        {
            this.store = store;
        }

        [HttpGet("{id}")]
        public ActionResult<Ticket> GetTicket(int id)
        {
            try
            {
                return Ok(store.GetTicket(id));
            }
            catch (ArgumentException) { return NotFound(); }

        }
        [HttpPost]
        public ActionResult<Ticket> CreateTicket([FromBody] CreateTicketRequest request)
        {
            try
            {
                var ticket = new Ticket(store.GetNextId(), request.Title, request.Description, request.ProjectId, request.DueDate, request.Priority);
                store.AddTicket(ticket);
                return CreatedAtAction(nameof(GetTicket), new { ticket.Id }, ticket);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("queue")]
        public List<Ticket> GetQueue() => store.GetCurrentTickets();

        [HttpPut("{id}")]
        public ActionResult<Ticket> UpdateTicket(int id, [FromBody] UpdateTicketRequest request)
        {
            Ticket ticket;
            try
            {
                ticket = store.GetTicket(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            try
            {
                ticket.Update(request.Title, request.Description, request.DueDate, request.ProjectId);
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
            Ticket ticket;
            try
            {
                ticket = store.GetTicket(id);
                ticket.StartProgress();
                return Ok(ticket);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
        }
        [HttpPost("{id}/resolve")]
        public ActionResult<Ticket> Resolve(int id)
        {
            Ticket ticket;
            try
            {
                ticket = store.GetTicket(id);
                ticket.Resolve();
                return Ok(ticket);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
        }
        [HttpPost("{id}/close")]
        public ActionResult<Ticket> Close(int id)
        {
            Ticket ticket;
            try
            {
                ticket = store.GetTicket(id);
                ticket.Close();
                return Ok(ticket);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
        }
        [HttpPost("{id}/reopen")]
        public ActionResult<Ticket> Reopen(int id)
        {
            Ticket ticket;
            try
            {
                ticket = store.GetTicket(id);
                ticket.Reopen();
                return Ok(ticket);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTicket(int id) => store.DeleteTicket(id) ? NoContent() : NotFound();
        [HttpGet]
        public List<Ticket> GetAll(TicketStatus? status, TicketPriority? priority)
        {
            return store.GetTickets(status, priority);
        }
    }
}
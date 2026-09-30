using Microsoft.AspNetCore.Mvc;
using TicketApp.Api.Models;
using TicketApp.Api.Services;
namespace TicketApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService ticketService;
        public TicketsController(ITicketService ticketService) { this.ticketService = ticketService; }

        [HttpGet("{id}")]
        public ActionResult<Ticket> GetTicket(int id)
        {
            try
            {
                return Ok(ticketService.GetTicket(id));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

        }
        [HttpPost]
        public ActionResult<Ticket> CreateTicket([FromBody] CreateTicketRequest request)
        {
            try
            {
                var ticket = ticketService.Create(request);

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
            return ticketService.GetQueue();
        }

        [HttpPut("{id}")]
        public ActionResult<Ticket> UpdateTicket(int id, [FromBody] UpdateTicketRequest request)
        {

            try
            {
                return Ok(ticketService.Update(id, request));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("{id}/start")]
        public ActionResult<Ticket> StartProgress(int id)
        {
            try
            {
                return Ok(ticketService.StartProgress(id));
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPost("{id}/resolve")]
        public ActionResult<Ticket> Resolve(int id)
        {
            try
            {
                return Ok(ticketService.Resolve(id));
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPost("{id}/close")]
        public ActionResult<Ticket> Close(int id)
        {
            try
            {
                return Ok(ticketService.Close(id));
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("{id}/reopen")]
        public ActionResult<Ticket> Reopen(int id)
        {
            try
            {
                return Ok(ticketService.Reopen(id));
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTicket(int id)
        {
            try
            {
                ticketService.Delete(id);
                return NoContent();
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet]
        public List<Ticket> GetAll(TicketStatus? status, TicketPriority? priority, bool overdue)
        {
            return ticketService.GetAll(status, priority, overdue);
        }
    }
}
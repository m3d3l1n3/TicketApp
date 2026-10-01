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
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            try
            {
                return Ok(await ticketService.GetTicket(id));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

        }
        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket([FromBody] CreateTicketRequest request)
        {
            try
            {
                var ticket = await ticketService.Create(request);

                return CreatedAtAction(nameof(GetTicket), new { ticket.Id }, ticket);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("queue")]
        public async Task<List<Ticket>> GetQueue()
        {
            return await ticketService.GetQueue();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Ticket>> UpdateTicket(int id, [FromBody] UpdateTicketRequest request)
        {

            try
            {
                return Ok(await ticketService.Update(id, request));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("{id}/start")]
        public async Task<ActionResult<Ticket>> StartProgress(int id)
        {
            try
            {
                return Ok(await ticketService.StartProgress(id));
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
        public async Task<ActionResult<Ticket>> Resolve(int id)
        {
            try
            {
                return Ok(await ticketService.Resolve(id));
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
        public async Task<ActionResult<Ticket>> Close(int id)
        {
            try
            {
                return Ok(await ticketService.Close(id));
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
        public async Task<ActionResult<Ticket>> Reopen(int id)
        {
            try
            {
                return Ok(await ticketService.Reopen(id));
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
        public async Task<IActionResult> DeleteTicket(int id)
        {
            try
            {
                await ticketService.Delete(id);
                return NoContent();
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet]
        public async Task<List<Ticket>> GetAll(TicketStatus? status, TicketPriority? priority, bool overdue)
        {
            return await ticketService.GetAll(status, priority, overdue);
        }
    }
}
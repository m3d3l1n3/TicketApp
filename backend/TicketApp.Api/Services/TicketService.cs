using TicketApp.Api.Models;
using TicketApp.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace TicketApp.Api.Services;

public class TicketService : ITicketService
{
    private readonly AppDbContext context;
    public TicketService(AppDbContext context) { this.context = context; }

    public async Task<Ticket> GetTicket(int id)
    {
        var ticket = await context.Tickets.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        if (ticket is null)
            throw new ArgumentException($"Ticket with {id} does not exist");
        return ticket;
    }
    public async Task<Ticket> Create(CreateTicketRequest request)
    {

        if (!await context.Projects.AnyAsync(p => p.Id == request.ProjectId))
            throw new ArgumentException("Project does not exist.");
        var ticket = new Ticket(request.Title, request.Description, request.ProjectId, request.DueDate, request.Priority);
        context.Tickets.Add(ticket);
        await context.SaveChangesAsync();

        return ticket;
    }
    public async Task<Ticket> Update(int id, UpdateTicketRequest request)
    {
        var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
        if (ticket is null)
            throw new ArgumentException($"Ticket with {id} does not exist");

        ticket.Update(request.Title, request.Description, request.DueDate, request.ProjectId);
        await context.SaveChangesAsync();
        return ticket;

    }
    public async Task Delete(int id)
    {
        int deleted = await context.Tickets.Where(t => t.Id == id).ExecuteDeleteAsync();
        if (deleted == 0)
            throw new ArgumentException("Ticket to delete not found");

    }
    public async Task<Ticket> StartProgress(int id)
    {
        var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
        if (ticket is null)
            throw new ArgumentException($"Ticket with {id} does not exist");

        ticket.StartProgress();
        await context.SaveChangesAsync();
        return ticket;
    }
    public async Task<Ticket> Resolve(int id)
    {
        var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
        if (ticket is null)
            throw new ArgumentException($"Ticket with {id} does not exist");

        ticket.Resolve();
        await context.SaveChangesAsync();
        return ticket;
    }
    public async Task<Ticket> Close(int id)
    {
        var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
        if (ticket is null)
            throw new ArgumentException($"Ticket with {id} does not exist");

        ticket.Close();
        await context.SaveChangesAsync();
        return ticket;

    }
    public async Task<Ticket> Reopen(int id)
    {
        var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
        if (ticket is null)
            throw new ArgumentException($"Ticket with {id} does not exist");

        ticket.Reopen();
        await context.SaveChangesAsync();
        return ticket;
    }
    public async Task<List<Ticket>> GetQueue()
    {
        return await context.Tickets.AsNoTracking().Where(t => t.Status != TicketStatus.Resolved && t.Status != TicketStatus.Closed).OrderByDescending(t => t.Priority).ThenBy(t => !t.DueDate.HasValue).ThenBy(t => t.DueDate).ToListAsync();
    }

    public async Task<List<Ticket>> GetAll(TicketStatus? status, TicketPriority? priority, bool overdue)
    {
        IQueryable<Ticket> query = context.Tickets.AsNoTracking();
        if (status is not null) query = query.Where(t => t.Status == status);
        if (priority is not null) query = query.Where(t => t.Priority == priority);
        if (overdue) query = query.Where(t => t.DueDate < DateOnly.FromDateTime(DateTime.Today) && t.Status != TicketStatus.Closed);
        return await query.ToListAsync();
    }

}
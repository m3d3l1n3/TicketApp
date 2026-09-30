using TicketApp.Api.Models;
using TicketApp.Api.Data;
using Microsoft.EntityFrameworkCore;
namespace TicketApp.Api.Services;

public class TicketService : ITicketService
{
    private readonly AppDbContext context;
    public TicketService(AppDbContext context) { this.context = context; }

    public Ticket GetTicket(int id)
    {
        var ticket = context.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == id);
        if (ticket is null)
            throw new ArgumentException($"Ticket with {id} does not exist");
        return ticket;
    }
    public Ticket Create(CreateTicketRequest request)
    {

        if (!context.Projects.Any(p => p.Id == request.ProjectId))
            throw new ArgumentException("Project does not exist.");
        var ticket = new Ticket(request.Title, request.Description, request.ProjectId, request.DueDate, request.Priority);
        context.Tickets.Add(ticket);
        context.SaveChanges();

        return ticket;
    }
    public Ticket Update(int id, UpdateTicketRequest request)
    {
        var ticket = context.Tickets.FirstOrDefault(t => t.Id == id);
        if (ticket is null)
            throw new ArgumentException($"Ticket with {id} does not exist");

        ticket.Update(request.Title, request.Description, request.DueDate, request.ProjectId);
        context.SaveChanges();
        return ticket;

    }
    public void Delete(int id)
    {
        int deleted = context.Tickets.Where(t => t.Id == id).ExecuteDelete();
        if (deleted == 0)
            throw new ArgumentException("Ticket to delete not found");

    }
    public Ticket StartProgress(int id)
    {
        var ticket = context.Tickets.FirstOrDefault(t => t.Id == id);
        if (ticket is null)
            throw new ArgumentException($"Ticket with {id} does not exist");

        ticket.StartProgress();
        context.SaveChanges();
        return ticket;
    }
    public Ticket Resolve(int id)
    {
        var ticket = context.Tickets.FirstOrDefault(t => t.Id == id);
        if (ticket is null)
            throw new ArgumentException($"Ticket with {id} does not exist");

        ticket.Resolve();
        context.SaveChanges();
        return ticket;
    }
    public Ticket Close(int id)
    {
        var ticket = context.Tickets.FirstOrDefault(t => t.Id == id);
        if (ticket is null)
            throw new ArgumentException($"Ticket with {id} does not exist");

        ticket.Close();
        context.SaveChanges();
        return ticket;

    }
    public Ticket Reopen(int id)
    {
        var ticket = context.Tickets.FirstOrDefault(t => t.Id == id);
        if (ticket is null)
            throw new ArgumentException($"Ticket with {id} does not exist");

        ticket.Reopen();
        context.SaveChanges();
        return ticket;
    }
    public List<Ticket> GetQueue()
    {
        return context.Tickets.AsNoTracking().Where(t => t.Status != TicketStatus.Resolved && t.Status != TicketStatus.Closed).OrderByDescending(t => t.Priority).ThenBy(t => !t.DueDate.HasValue).ThenBy(t => t.DueDate).ToList();
    }

    public List<Ticket> GetAll(TicketStatus? status, TicketPriority? priority, bool overdue)
    {
        IQueryable<Ticket> query = context.Tickets.AsNoTracking();
        if (status is not null) query = query.Where(t => t.Status == status);
        if (priority is not null) query = query.Where(t => t.Priority == priority);
        if (overdue) query = query.Where(t => t.DueDate < DateOnly.FromDateTime(DateTime.Today) && t.Status != TicketStatus.Closed);
        return query.ToList();
    }

}
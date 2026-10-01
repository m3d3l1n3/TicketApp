using TicketApp.Api.Models;

namespace TicketApp.Api.Services;

public interface ITicketService
{
    Task<Ticket> GetTicket(int id);
    Task<Ticket> Create(CreateTicketRequest request);
    Task<Ticket> Update(int id, UpdateTicketRequest request);
    Task Delete(int id);
    Task<Ticket> StartProgress(int id);
    Task<Ticket> Resolve(int id);
    Task<Ticket> Close(int id);
    Task<Ticket> Reopen(int id);
    Task<List<Ticket>> GetQueue();
    Task<List<Ticket>> GetAll(TicketStatus? status, TicketPriority? priority, bool overdue);


}
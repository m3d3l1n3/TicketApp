using TicketApp.Api.Models;

namespace TicketApp.Api.Services;

public interface ITicketService
{
    Ticket GetTicket(int id);
    Ticket Create(CreateTicketRequest request);
    Ticket Update(int id, UpdateTicketRequest request);
    void Delete(int id);
    Ticket StartProgress(int id);
    Ticket Resolve(int id);
    Ticket Close(int id);
    Ticket Reopen(int id);
    List<Ticket> GetQueue();
    List<Ticket> GetAll(TicketStatus? status, TicketPriority? priority, bool overdue);


}
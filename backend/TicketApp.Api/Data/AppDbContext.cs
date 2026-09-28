using Microsoft.EntityFrameworkCore;
using TicketApp.Api.Models;
namespace TicketApp.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Project> Projects => Set<Project>();
}
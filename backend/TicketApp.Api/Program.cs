using TicketApp.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
TicketStore store = new();
Ticket ticket = new(1, "Opening error", "issue when trying to open")
{
    ProjectId = 1
};
store.AddTicket(ticket);
Ticket ticket2 = new(2, "Map rendering error", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis lorem lectus, porta ut efficitur ac, imperdiet sed nisl. Praesent aliquet lectus ac auctor posuere. Donec vitae facilisis turpis. Aenean commodo tincidunt leo, fermentum fermentum purus molestie eget. Praesent porta ante rhoncus lacus varius rhoncus ut eu sem.")
{
    ProjectId = 3
};
ticket2.StartProgress();
store.AddTicket(ticket2);
Ticket ticket3 = new(3, "Inventory related issue", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis lorem lectus, porta ut efficitur ac, imperdiet sed nisl. Praesent aliquet lectus ac auctor posuere. Donec vitae facilisis turpis. Aenean commodo tincidunt leo, fermentum fermentum purus molestie eget. Praesent porta ante rhoncus lacus varius rhoncus ut eu sem.")
{
    ProjectId = 4
};
ticket3.DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));
store.AddTicket(ticket3);
Ticket ticket4 = new(4, "Subtitles not rendering", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis lorem lectus, porta ut efficitur ac, imperdiet sed nisl. Praesent aliquet lectus ac auctor posuere. Donec vitae facilisis turpis. Aenean commodo tincidunt leo, fermentum fermentum purus molestie eget. Praesent porta ante rhoncus lacus varius rhoncus ut eu sem.");
ticket4.StartProgress();
ticket4.Resolve();
store.AddTicket(ticket4);
Ticket ticket5 = new(5, "Collision does not work", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis lorem lectus, porta ut efficitur ac, imperdiet sed nisl. Praesent aliquet lectus ac auctor posuere. Donec vitae facilisis turpis. Aenean commodo tincidunt leo, fermentum fermentum purus molestie eget. Praesent porta ante rhoncus lacus varius rhoncus ut eu sem.");
ticket5.StartProgress();
ticket5.DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));
ticket5.Resolve();
ticket5.Close();
store.AddTicket(ticket5);
Ticket ticket6 = new(6, "Phase thru walls", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis lorem lectus, porta ut efficitur ac, imperdiet sed nisl. Praesent aliquet lectus ac auctor posuere. Donec vitae facilisis turpis. Aenean commodo tincidunt leo, fermentum fermentum purus molestie eget. Praesent porta ante rhoncus lacus varius rhoncus ut eu sem.");
// {
// ProjectId = 1,
// Priority = TicketPriority.Medium
// };
store.AddTicket(ticket6);

var tickets = store.GetTickets();
var dir = store.GetTicketCountsByStatus();
foreach (var item in dir)
{
    Console.WriteLine($"Tickets by status: {item.Key}, {item.Value}");
}
var dir2 = store.GetTicketsWithPriority(TicketPriority.Medium);
foreach (var item in dir2)
{
    Console.WriteLine($"Tickets with priority medium: {item.Id}, {item.Priority}");
}
var dir3 = store.GetTicketsOverdue();
foreach (var item in dir3)
{
    Console.WriteLine($"Tickets overdue: {item.Title}, {item.Id}");
}
var dir4 = store.GetProjectReport(1);
Console.WriteLine($"PR: {dir4.ProjectId} {dir4.OpenTickets} {dir4.OverdueTickets} {dir4.EarliestDueDate}");
var d = store.GetAllProjectReports();
foreach (var rep in d)
{
    Console.WriteLine($"PR: {rep.ProjectId} {rep.OpenTickets} {rep.OverdueTickets} {rep.EarliestDueDate}");

}
app.Run();




using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using TicketApp.Api.Models;

namespace TicketApp.Tests;

public class UnitTestTicketStore
{
    // //Part B
    // [Fact]
    // public void Test_Correct_Order()
    // {
    //     TicketStore store = new();
    //     Ticket ticket = new Ticket(1, "Neque porro", "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit")
    //     {
    //         DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-1))
    //     };

    //     Ticket ticket1 = new(2, "Vestibulum dictum arcu et", "Vestibulum dictum arcu et ex laoreet, a malesuada lorem vehicula. Donec non vulputate quam.")
    //     {
    //         Priority = TicketPriority.High,
    //         DueDate = DateOnly.FromDateTime(DateTime.Today)

    //     };
    //     Ticket ticket2 = new(3, "Phasellus tempor ", "Phasellus tempor nec risus at posuere. Duis nec suscipit tellus, sit amet eleifend erat.");
    //     store.AddTicket(ticket);
    //     store.AddTicket(ticket1);
    //     store.AddTicket(ticket2);

    //     var results = store.GetCurrentTickets();
    //     Assert.True(results[0].Id == 2);
    //     Assert.True(results[1].Id == 1);
    //     Assert.True(results[2].Id == 3);
    // }

    // [Fact]
    // public void Test_Project_Summaries()
    // {
    //     TicketStore store = new();
    //     Ticket ticket = new Ticket(1, "Neque porro", "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit")
    //     {
    //         DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
    //         ProjectId = 2
    //     };

    //     Ticket ticket1 = new(2, "Vestibulum dictum arcu et", "Vestibulum dictum arcu et ex laoreet, a malesuada lorem vehicula. Donec non vulputate quam.")
    //     {
    //         Priority = TicketPriority.High,
    //         DueDate = DateOnly.FromDateTime(DateTime.Today),
    //         ProjectId = 4

    //     };
    //     Ticket ticket2 = new(3, "Phasellus tempor ", "Phasellus tempor nec risus at posuere. Duis nec suscipit tellus, sit amet eleifend erat.")
    //     {
    //         ProjectId = 2
    //     };
    //     store.AddTicket(ticket);
    //     store.AddTicket(ticket1);
    //     store.AddTicket(ticket2);

    //     var res1 = store.GetProjectReport(2);
    //     Assert.True(res1.Item1[0] == 2);
    //     Assert.True(res1.Item2 == DateOnly.FromDateTime(DateTime.Today.AddDays(-1)));

    //     res1 = store.GetProjectReport(4);
    //     Assert.True(res1.Item2.HasValue);
    // }

    // [Fact]
    // public void Test_Escaladation()
    // {
    //     Ticket ticket = new Ticket(1, "Neque porro", "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit")
    //     {
    //         DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-7)),
    //         ProjectId = 2
    //     };

    //     Ticket ticket1 = new(2, "Vestibulum dictum arcu et", "Vestibulum dictum arcu et ex laoreet, a malesuada lorem vehicula. Donec non vulputate quam.")
    //     {
    //         Priority = TicketPriority.Critical,
    //         DueDate = DateOnly.FromDateTime(DateTime.Today),
    //         ProjectId = 4

    //     };
    //     try
    //     {
    //         ticket1.PriorityEscalation();
    //     }
    //     catch (Exception e)
    //     {

    //     }
    //     ticket.PriorityEscalation();

    // }



}


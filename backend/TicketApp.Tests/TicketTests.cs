using TicketApp.Api.Models;

namespace TicketApp.Tests;

public class TicketTests
{
    [Fact]
    public void Constructor_SetDefault()
    {
        Ticket ticket1 = new Ticket("title", "desc", 1, null);
        Assert.Equal(TicketStatus.Open, ticket1.Status);
        Assert.NotEqual(default(DateTime), ticket1.CreatedAt);
        Assert.Equal(TicketPriority.Low, ticket1.Priority);

    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidTitle_Throws(string? title)
    {
        Assert.Throws<ArgumentException>(() => new Ticket(title, "desc", 1, null));
    }

    [Fact]
    public void Close_WhenOpen_Throws()
    {
        Ticket ticket1 = new Ticket("title", "desc", 1, null);
        Assert.Throws<InvalidOperationException>(() => ticket1.Close());
    }
    [Fact]
    public void Resolve_WhenOpen_Throws()
    {
        Ticket ticket1 = new Ticket("title", "desc", 1, null);
        Assert.Throws<InvalidOperationException>(() => ticket1.Resolve());
    }
    [Fact]
    public void Reopen_WhenOpen_Throws()
    {
        Ticket ticket1 = new Ticket("title", "desc", 1, null);
        Assert.Throws<InvalidOperationException>(() => ticket1.Reopen());
    }
    [Fact]
    public void StartProgress_WhenClosed_Throws()
    {
        Ticket ticket1 = new Ticket("title", "desc", 1, null);

        ticket1.StartProgress();
        ticket1.Resolve();
        ticket1.Close();

        Assert.Throws<InvalidOperationException>(() => ticket1.StartProgress());
    }
    [Fact]
    public void Correct_State_Transition()
    {
        Ticket ticket1 = new Ticket("title", "desc", 1, null);
        Assert.Equal(TicketStatus.Open, ticket1.Status);
        ticket1.StartProgress();
        Assert.Equal(TicketStatus.InProgress, ticket1.Status);
        ticket1.Resolve();
        Assert.Equal(TicketStatus.Resolved, ticket1.Status);
        ticket1.Close();
        Assert.Equal(TicketStatus.Closed, ticket1.Status);
        ticket1.Reopen();
        Assert.Equal(TicketStatus.Open, ticket1.Status);
    }

    [Fact]
    public void PriorityEscalation_OldOpenTicket_EscalatesPriority()
    {
        Ticket ticket1 = new Ticket("title", "desc", 1, null, TicketPriority.Low, createdAt: DateTime.UtcNow.AddDays(-8));
        Assert.Equal(TicketStatus.Open, ticket1.Status);
        ticket1.PriorityEscalation(DateTime.UtcNow);
        Assert.Equal(TicketPriority.Medium, ticket1.Priority);
    }
    [Fact]
    public void PriorityEscalation_OldOpenTicket_MaximumPriority_Throws()
    {
        Ticket ticket1 = new Ticket("title", "desc", 1, null, TicketPriority.Critical, createdAt: DateTime.UtcNow.AddDays(-8));
        Assert.Equal(TicketStatus.Open, ticket1.Status);
        Assert.Equal(TicketPriority.Critical, ticket1.Priority);

        Assert.Throws<InvalidOperationException>(() => ticket1.PriorityEscalation(DateTime.UtcNow));
    }

    [Fact]
    public void PriorityEscalation_NotOldEnough_OpenTicket_Throws()
    {
        Ticket ticket1 = new Ticket("title", "desc", 1, null, TicketPriority.Low, createdAt: DateTime.UtcNow.AddDays(-3));
        Assert.Equal(TicketStatus.Open, ticket1.Status);
        Assert.Equal(TicketPriority.Low, ticket1.Priority);
        Assert.Throws<InvalidOperationException>(() => ticket1.PriorityEscalation(DateTime.UtcNow));
    }
    [Fact]
    public void PriorityEscalation_OldOpenTicket_NotOpenStatus_Throws()
    {
        Ticket ticket1 = new Ticket("title", "desc", 1, null, TicketPriority.Low, createdAt: DateTime.UtcNow.AddDays(-8));
        ticket1.StartProgress();
        Assert.Equal(TicketStatus.InProgress, ticket1.Status);
        Assert.Equal(TicketPriority.Low, ticket1.Priority);

        Assert.Throws<InvalidOperationException>(() => ticket1.PriorityEscalation(DateTime.UtcNow));
    }
}


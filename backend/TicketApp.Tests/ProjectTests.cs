using TicketApp.Api.Models;

namespace TicketApp.Tests;

public class ProjectTests
{

    [Fact]
    public void Constructor_SetDefault()
    {
        Project project = new Project("title", "desc");
        Assert.Equal("title", project.Name);
        Assert.Equal("desc", project.Description);

    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidTitle_Throws(string? title)
    {
        Assert.Throws<ArgumentException>(() => new Project(title, "desc"));
    }
}
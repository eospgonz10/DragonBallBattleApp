using DragonBallBattles.API.Controllers;
using DragonBallBattles.Application.Services;
using DragonBallBattles.Domain.Entities;
using DragonBallBattles.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DragonBallBattles.Tests.Unit;

public class BattlesControllerTests
{
    private readonly BattlesController _controller;
    private readonly Mock<IBattleScheduler> _battleSchedulerMock;

    public BattlesControllerTests()
    {
        _battleSchedulerMock = new Mock<IBattleScheduler>();
        _controller = new BattlesController(_battleSchedulerMock.Object);
    }

    [Fact]
    public async Task ScheduleBattles_ReturnsOk_WithBattles()
    {
        // Arrange
        var battles = new List<Battle>
        {
            new Battle { Fighter1 = "Goku", Fighter2 = "Vegeta", Date = DateTime.UtcNow.AddDays(30) },
            new Battle { Fighter1 = "Gohan", Fighter2 = "Piccolo", Date = DateTime.UtcNow.AddDays(30) }
        };
        
        _battleSchedulerMock.Setup(s => s.ScheduleBattlesAsync(4, It.IsAny<DateTime>())).ReturnsAsync(battles);

        // Act
        var result = await _controller.ScheduleBattles(4);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}

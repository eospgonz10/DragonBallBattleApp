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
    private readonly Mock<ICharacterRepository> _characterRepositoryMock;

    public BattlesControllerTests()
    {
        _battleSchedulerMock = new Mock<IBattleScheduler>();
        _characterRepositoryMock = new Mock<ICharacterRepository>();
        _controller = new BattlesController(_battleSchedulerMock.Object, _characterRepositoryMock.Object);
    }

    [Fact]
    public async Task ScheduleBattles_ReturnsOk_WithBattles()
    {
        // Arrange
        var characters = new List<Character>
        {
            new Character { Id = 1, Name = "Goku" },
            new Character { Id = 2, Name = "Vegeta" },
            new Character { Id = 3, Name = "Gohan" },
            new Character { Id = 4, Name = "Piccolo" }
        };
        _characterRepositoryMock.Setup(r => r.GetCharactersAsync(4)).ReturnsAsync(characters);
        _battleSchedulerMock.Setup(s => s.ScheduleBattles(characters, It.IsAny<DateTime>())).Returns(new List<Battle>
        {
            new Battle { Fighter1 = "Goku", Fighter2 = "Vegeta", Date = DateTime.UtcNow.AddDays(30) },
            new Battle { Fighter1 = "Gohan", Fighter2 = "Piccolo", Date = DateTime.UtcNow.AddDays(30) }
        });

        // Act
        var result = await _controller.ScheduleBattles(4);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}

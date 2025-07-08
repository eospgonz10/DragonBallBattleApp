using DragonBallBattles.Infrastructure.Repositories;
using DragonBallBattles.Application.Services;
using DragonBallBattles.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace DragonBallBattles.Tests.Integration;

public class CharacterRepositoryIntegrationTests
{
    [Fact]
    public async Task GetCharactersAsync_Returns_ValidCharacters()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string> {
            { "DragonBallApi:BaseUrl", "https://dragonball-api.com" },
            { "DragonBallApi:CharactersEndpoint", "/api/characters" }
        };
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
        var httpClient = new HttpClient();
        ICharacterRepository repository = new CharacterRepository(httpClient, configuration);

        // Act
        var characters = await repository.GetCharactersAsync(4);

        // Assert
        Assert.NotNull(characters);
        Assert.Equal(4, characters.Count);
        Assert.All(characters, c => Assert.False(string.IsNullOrWhiteSpace(c.Name)));
    }
}

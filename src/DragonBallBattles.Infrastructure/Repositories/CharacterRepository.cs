using DragonBallBattles.Domain.Entities;
using DragonBallBattles.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace DragonBallBattles.Infrastructure.Repositories;

public class CharacterRepository : ICharacterRepository
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _endpoint;

    public CharacterRepository(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["DragonBallApi:BaseUrl"] ?? string.Empty;
        _endpoint = configuration["DragonBallApi:CharactersEndpoint"] ?? string.Empty;
    }

    public async Task<List<Character>> GetCharactersAsync(int count)
    {
        var url = $"{_baseUrl}{_endpoint}?page=1&limit={count}";
        var response = await _httpClient.GetFromJsonAsync<DragonBallApiResponse>(url);
        return response?.Items?.Select(x => new Character { Id = x.Id, Name = x.Name }).ToList() ?? new List<Character>();
    }

    private class DragonBallApiResponse
    {
        public List<CharacterItem> Items { get; set; } = new();
    }
    private class CharacterItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

using DragonBallBattles.Domain.Entities;

namespace DragonBallBattles.Application.DTOs;

public class CharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

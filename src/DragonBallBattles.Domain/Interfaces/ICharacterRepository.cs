namespace DragonBallBattles.Domain.Interfaces;

public interface ICharacterRepository
{
    Task<List<Entities.Character>> GetCharactersAsync(int count);
}

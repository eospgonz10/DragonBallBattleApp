using DragonBallBattles.Domain.Entities;
using DragonBallBattles.Domain.Interfaces;
using DragonBallBattles.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace DragonBallBattles.Application.Services;

public class BattleSchedulerService : IBattleScheduler
{
    private readonly ILogger<BattleSchedulerService> _logger;
    private readonly ICharacterRepository _characterRepository;

    public BattleSchedulerService(ILogger<BattleSchedulerService> logger, ICharacterRepository characterRepository)
    {
        _logger = logger;
        _characterRepository = characterRepository;
    }

    public async Task<List<Battle>> ScheduleBattlesAsync(int numeroParticipantes, DateTime startDate)
    {
        _logger.LogInformation("Attempting to schedule battles for {NumeroParticipantes} participants starting from {StartDate}", numeroParticipantes, startDate);

        ValidateParticipantCount(numeroParticipantes);

        var characters = await _characterRepository.GetCharactersAsync(numeroParticipantes);
        
        ValidateCharacterList(characters, numeroParticipantes);

        var random = new Random();
        var shuffled = characters.OrderBy(x => random.Next()).ToList();
        var battles = new List<Battle>();
        int dayOffset = 0;
        
        _logger.LogDebug("Shuffled characters: {CharacterNames}", string.Join(", ", shuffled.Select(c => c.Name)));

        for (int i = 0; i < shuffled.Count; i += 2)
        {
            if ((i / 2) % 2 == 0 && i != 0)
                dayOffset++;
            var date = startDate.AddDays(dayOffset);
            var battle = new Battle
            {
                Fighter1 = shuffled[i].Name,
                Fighter2 = shuffled[i + 1].Name,
                Date = date
            };
            battles.Add(battle);
            _logger.LogDebug("Scheduled battle: {Fighter1} vs {Fighter2} on {BattleDate}", battle.Fighter1, battle.Fighter2, battle.Date);
        }

        _logger.LogInformation("Successfully scheduled {BattleCount} battles.", battles.Count);
        return battles;
    }

    private void ValidateParticipantCount(int numeroParticipantes)
    {
        if (numeroParticipantes <= 0)
        {
            _logger.LogError("Battle scheduling failed: Invalid participant count. NumeroParticipantes: {NumeroParticipantes}", numeroParticipantes);
            throw new DomainException("El número de participantes debe ser mayor a 0.");
        }
        
        if (numeroParticipantes % 2 != 0)
        {
            _logger.LogError("Battle scheduling failed: Number of participants ({NumeroParticipantes}) must be even.", numeroParticipantes);
            throw new DomainException("El número de participantes debe ser par.");
        }
        
        if (numeroParticipantes > 16)
        {
            _logger.LogError("Battle scheduling failed: Maximum number of participants is 16, but got {NumeroParticipantes}.", numeroParticipantes);
            throw new DomainException("El número máximo de participantes es 16.");
        }
    }

    private void ValidateCharacterList(List<Character> characters, int expectedCount)
    {
        if (characters == null || characters.Count <= 0)
        {
            _logger.LogError("Battle scheduling failed: No characters returned from repository. Expected: {ExpectedCount}, Got: {ActualCount}", expectedCount, characters?.Count ?? 0);
            throw new DomainException("No se pudieron obtener personajes del repositorio.");
        }

        if (characters.Count != expectedCount)
        {
            _logger.LogWarning("Character count mismatch: Expected {ExpectedCount}, Got {ActualCount}", expectedCount, characters.Count);
        }
    }
}

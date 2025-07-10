using DragonBallBattles.Domain.Entities;
using DragonBallBattles.Domain.Interfaces;
using DragonBallBattles.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace DragonBallBattles.Application.Services;

public class BattleSchedulerService : IBattleScheduler
{
    private readonly ILogger<BattleSchedulerService> _logger;

    public BattleSchedulerService(ILogger<BattleSchedulerService> logger)
    {
        _logger = logger;
    }

    public List<Battle> ScheduleBattles(List<Character> characters, DateTime startDate)
    {
        _logger.LogInformation("Attempting to schedule battles for {CharacterCount} characters starting from {StartDate}", characters?.Count ?? 0, startDate);

        if (characters == null || characters.Count <= 0)
        {
            _logger.LogError("Battle scheduling failed: Invalid character list. Characters: {CharacterCount}", characters?.Count ?? 0);
            throw new DomainException("El número de participantes debe ser mayor a 0 y la lista de personajes no puede estar vacía.");
        }
        if (characters.Count % 2 != 0)
        {
            _logger.LogError("Battle scheduling failed: Number of participants ({CharacterCount}) must be even.", characters.Count);
            throw new DomainException("El número de participantes debe ser par.");
        }
        if (characters.Count > 16)
        {
            _logger.LogError("Battle scheduling failed: Maximum number of participants is 16, but got {CharacterCount}.", characters.Count);
            throw new DomainException("El número máximo de participantes es 16.");
        }

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
}

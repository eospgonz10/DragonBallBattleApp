using DragonBallBattles.Domain.Entities;
using DragonBallBattles.Domain.Interfaces;
using DragonBallBattles.Domain.Exceptions;

namespace DragonBallBattles.Application.Services;

public class BattleSchedulerService : IBattleScheduler
{
    public List<Battle> ScheduleBattles(List<Character> characters, DateTime startDate)
    {
        if (characters == null || characters.Count == 0)
            throw new DomainException("La lista de personajes no puede estar vacía.");
        if (characters.Count % 2 != 0)
            throw new DomainException("El número de participantes debe ser par.");
        if (characters.Count > 16)
            throw new DomainException("El número máximo de participantes es 16.");

        var random = new Random();
        var shuffled = characters.OrderBy(x => random.Next()).ToList();
        var battles = new List<Battle>();
        int dayOffset = 0;
        for (int i = 0; i < shuffled.Count; i += 2)
        {
            if ((i / 2) % 2 == 0 && i != 0)
                dayOffset++;
            var date = startDate.AddDays(dayOffset);
            battles.Add(new Battle
            {
                Fighter1 = shuffled[i].Name,
                Fighter2 = shuffled[i + 1].Name,
                Date = date
            });
        }
        return battles;
    }
}

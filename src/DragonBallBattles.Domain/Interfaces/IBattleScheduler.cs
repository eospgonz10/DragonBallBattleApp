namespace DragonBallBattles.Domain.Interfaces;

public interface IBattleScheduler
{
    List<Entities.Battle> ScheduleBattles(List<Entities.Character> characters, DateTime startDate);
}

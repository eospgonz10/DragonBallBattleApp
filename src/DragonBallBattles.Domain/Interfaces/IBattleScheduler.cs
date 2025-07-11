namespace DragonBallBattles.Domain.Interfaces;

public interface IBattleScheduler
{
    Task<List<Entities.Battle>> ScheduleBattlesAsync(int numeroParticipantes, DateTime startDate);
}

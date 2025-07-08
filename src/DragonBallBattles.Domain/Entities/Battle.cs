namespace DragonBallBattles.Domain.Entities;

public class Battle
{
    public string Fighter1 { get; set; } = string.Empty;
    public string Fighter2 { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    public string GetBattleName() => $"{Fighter1} vs {Fighter2}";
}

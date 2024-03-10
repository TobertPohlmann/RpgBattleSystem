namespace RpgBattleSystem.Characters;

public class StatusValueFactory
{
    public static StatusValue GetMaxHealthStatusValue()
    {
        StatusValue maxHealth = new();
        return maxHealth
            .WithLevelCurveFor(Attribute.Robustness, new LevelCurve(100, 2000, 10))
            .WithLevelCurveFor(Attribute.Endurance,new LevelCurve(0,200,0));
    }
}
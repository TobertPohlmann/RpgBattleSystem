namespace RpgBattleSystem.Characters;

public class StatusValueFactory
{
    public static StatusValue GetStatusValueOf(Status status)
    {
        switch (status)
        {
            case Status.MaxHealth: return GetMaxHealthStatusValue();
            case Status.StrikeForce: return GetStrikeForceStatusValue();
            case Status.Technique: return GetTechniqueStatusValue();
            case Status.Precision: return GetPrecisionStatusValue();
            case Status.StrikeDefense: return GetStrikeDefenseStatusValue();
            case Status.CutDefense: return GetCutDefenseStatusValue();
            case Status.PierceDefense: return GetPierceDefenseStatusValue();
            case Status.PoisonResistance: return GetPoisonResistanceStatusValue();
            case Status.HeatResistance: return GetHeatResistanceStatusValue();
            case Status.ColdResistance: return GetColdResistanceStatusValue();
            case Status.EquipLoad: return GetEquipLoadStatusValue();
            default: return GetSpeedStatusValue();
        }
    }
    
    private static StatusValue GetMaxHealthStatusValue()
    {
        StatusValue statusValue = new();
        return statusValue
            .WithLevelCurveFor(Attribute.Vitality, new LevelCurve(100, 2000, 70))
            .WithLevelCurveFor(Attribute.Endurance,new LevelCurve(0,400,0,CurveType.Linear));
    }

    private static StatusValue GetStrikeForceStatusValue()
    {
        StatusValue statusValue  = new();
        return statusValue
                .WithLevelCurveFor(Attribute.Strength,
                    new LevelCurve(10, 500, 50, CurveType.LateGrowth, 70));
    }
    
    private static StatusValue GetTechniqueStatusValue()
    {
        StatusValue statusValue = new();
        return statusValue
            .WithLevelCurveFor(Attribute.Dexterity,
                new LevelCurve(10, 500, 50, CurveType.LateGrowth, 70));
    }
    
    private static StatusValue GetPrecisionStatusValue()
    {
        StatusValue statusValue = new();
        return statusValue
            .WithLevelCurveFor(Attribute.Focus,
                new LevelCurve(10, 500, 50, CurveType.LateGrowth, 70));
    }
    
    private static StatusValue GetStrikeDefenseStatusValue()
    {
        StatusValue statusValue = new();
        return statusValue
            .WithLevelCurveFor(Attribute.Vitality,
                new LevelCurve(0, 70, 50, CurveType.EarlyGrowth)) 
            .WithLevelCurveFor(Attribute.Strength,
                new LevelCurve(0, 130, 0, CurveType.Linear))
            .WithLevelCurveFor(Attribute.Immunity,
                new LevelCurve(10, 300, 0, CurveType.Linear));
    }
    
    private static StatusValue GetCutDefenseStatusValue()
    {
        StatusValue statusValue = new();
        return statusValue
            .WithLevelCurveFor(Attribute.Vitality,
                new LevelCurve(0, 70, 50, CurveType.EarlyGrowth)) 
            .WithLevelCurveFor(Attribute.Focus,
                new LevelCurve(0, 130, 0, CurveType.Linear))
            .WithLevelCurveFor(Attribute.Immunity,
                new LevelCurve(10, 300, 0, CurveType.Linear));
    }
    
    private static StatusValue GetPierceDefenseStatusValue()
    {
        StatusValue statusValue = new();
        return statusValue
            .WithLevelCurveFor(Attribute.Vitality,
                new LevelCurve(0, 70, 50, CurveType.EarlyGrowth)) 
            .WithLevelCurveFor(Attribute.Focus,
                new LevelCurve(0, 130, 0, CurveType.Linear))
            .WithLevelCurveFor(Attribute.Immunity,
                new LevelCurve(10, 300, 0, CurveType.Linear));
    }
    
    private static StatusValue GetPoisonResistanceStatusValue()
    {
        StatusValue statusValue = new();
        return statusValue
            .WithLevelCurveFor(Attribute.Endurance,
                new LevelCurve(0, 100, 20, CurveType.Linear))
            .WithLevelCurveFor(Attribute.Immunity,
                new LevelCurve(10, 400, 60, CurveType.EarlyGrowth));
    }
    
    private static StatusValue GetHeatResistanceStatusValue()
    {
        StatusValue statusValue = new();
        return statusValue
            .WithLevelCurveFor(Attribute.Endurance,
                new LevelCurve(0, 70, 0, CurveType.Linear))
            .WithLevelCurveFor(Attribute.Immunity,
                new LevelCurve(10, 300, 50, CurveType.MidLevelGrowth))            
            .WithLevelCurveFor(Attribute.Strength,
                new LevelCurve(0, 130, 40, CurveType.MidLevelGrowth));
    }
    
    private static StatusValue GetColdResistanceStatusValue()
    {
        StatusValue statusValue = new();
        return statusValue
            .WithLevelCurveFor(Attribute.Endurance,
                new LevelCurve(0, 70, 20, CurveType.Linear))
            .WithLevelCurveFor(Attribute.Immunity,
                new LevelCurve(10, 300, 50, CurveType.MidLevelGrowth))
            .WithLevelCurveFor(Attribute.Focus,
                new LevelCurve(0, 130, 40, CurveType.MidLevelGrowth));
    }
    
    private static StatusValue GetEquipLoadStatusValue()
    {
        StatusValue statusValue = new();
        return statusValue
            .WithLevelCurveFor(Attribute.Endurance,
                new LevelCurve(200, 700, 60, CurveType.EarlyGrowth));
    }
    
    private static StatusValue GetSpeedStatusValue()
    {
        StatusValue statusValue = new();
        return statusValue
            .WithLevelCurveFor(Attribute.Swiftness,
                new LevelCurve(10, 430, 40, CurveType.LateGrowth, softCapLevel:75))
            .WithLevelCurveFor(Attribute.Dexterity,
                new LevelCurve(0, 70, 0, CurveType.Linear));

    }
}

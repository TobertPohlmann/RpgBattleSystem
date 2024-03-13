namespace RpgBattleSystem.Characters;

public class StatusValue
{
    public Dictionary<Attribute, LevelCurve> LevelCurves = new();

    public StatusValue WithLevelCurveFor(Attribute attribute, LevelCurve levelCurve)
    {
        LevelCurves[attribute] = levelCurve;
        return this;
    }

    public int GetCharacterBaseValue(CharacterBase characterBase)
    {
        int baseValue = 0;
        foreach (Attribute attribute in LevelCurves.Keys)
        {
            int level = characterBase.GetLevelFor(attribute);
            baseValue += LevelCurves[attribute].GetValueForLevel(level);
        }

        return baseValue;
    }

}
namespace RpgBattleSystem.Characters;

public class StatusValue
{
    private Dictionary<Attribute, LevelCurve> _levelCurves = new();

    public StatusValue WithLevelCurveFor(Attribute attribute, LevelCurve levelCurve)
    {
        _levelCurves[attribute] = levelCurve;
        return this;
    }

    private int GetCharacterBaseValue(CharacterBase characterBase)
    {
        int baseValue = 0;
        foreach (Attribute attribute in _levelCurves.Keys)
        {
            int level = characterBase.GetLevelFor(attribute);
            baseValue += _levelCurves[attribute].GetValueForLevel(level);
        }

        return baseValue;
    }

}
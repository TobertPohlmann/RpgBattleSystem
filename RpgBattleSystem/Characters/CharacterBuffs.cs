using RpgBattleSystem.Enums;

namespace RpgBattleSystem.Characters;

public class CharacterBuffs
{
    private Dictionary<Status,List<Buff>> _absoluteBuffs = new();
    private Dictionary<Status,List<Buff>> _relativeBuffs = new();


    public CharacterBuffs()
    {
        foreach (Status status in Enum.GetValues(typeof(Status)))
        {
            _absoluteBuffs[status] = new List<Buff>();
            _relativeBuffs[status] = new List<Buff>();
        }
    }
    
    public void AddBuff(Buff buff)
    {
        switch (buff.Mode)
        {
            case BuffMode.absolute: _absoluteBuffs[buff.Status].Add(buff);
                break;
            case BuffMode.relative: _relativeBuffs[buff.Status].Add(buff);
                break;
            default: throw new Exception("Buff mode " + buff.Mode + " is unknown.");
        }
    }

    public int GetModifiedValue(int baseValue, Status status)
    {
        int absoluteModifier = GetAbsoluteModifier(status);
        double relativeModifier = GetRelativeModifier(status);
        return (int) ((baseValue + absoluteModifier) * relativeModifier);
    }

    private int GetAbsoluteModifier(Status status)
    {
        return (int)_absoluteBuffs[status].Select(x => x.Amount).Sum();
    }
    
    private double GetRelativeModifier(Status status)
    {
        return _relativeBuffs[status].Select(x => x.Amount).Aggregate(1.0, (acc, val) => acc * val);
    }
}
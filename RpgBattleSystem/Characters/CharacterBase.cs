using RpgBattleSystem.Characters.StatusValues;
using RpgBattleSystem.Enums;
using Attribute = RpgBattleSystem.Enums.Attribute;

namespace RpgBattleSystem.Characters;

public class CharacterBase
{
    public string Name { get; set; }
    public Sex Sex { get; set; }
    public Heritage Heritage { get; set; }
    
    private BoundedValue _level = new(1,1,200);
    private Dictionary<Attribute,BoundedValue> _attributeLevels = new ();
    private Dictionary<Status, StatusValue> _statusValues = new();

    public CharacterBase(string name, Sex sex, Heritage heritage)
    {
        Name = name;
        Sex = sex;
        Heritage = heritage;
        foreach (Attribute attribute in Enum.GetValues(typeof(Attribute)))
        {
            _attributeLevels[attribute] = new(1);
        }

        foreach (Status status in Enum.GetValues(typeof(Status)))
        {
            _statusValues[status] = StatusValueFactory.GetStatusValueOf(status);
        }
        
    }

    public int GetLevel()
    {
        return _level.CurrentValue;
    }

    public int GetLevelFor(Attribute attribute)
    {
        return _attributeLevels[attribute].CurrentValue;
    }

    public int GetStatusValueFor(Status status)
    {
        return _statusValues[status].GetCharacterBaseValue(this);
    }

    public void IncreaseLevelFor(Attribute attribute,int increment)
    {
        BoundedValue attributeLevel = _attributeLevels[attribute];
        if (_level.CanBeIncrementedBy(increment) && attributeLevel.CanBeIncrementedBy(increment))
        {
            _level.IncreaseValueBy(increment);
            attributeLevel.IncreaseValueBy(increment);
        }
    }
}
namespace RpgBattleSystem.Characters;

public class CharacterBase
{
    public string Name { get; set; }

    private BoundedValue _level = new(1,1,200);
    private Dictionary<Attribute,BoundedValue> _attributeLevels = new ();
    
    public CharacterBase(string name)
    {
        Name = name;
        foreach (Attribute attribute in Enum.GetValues(typeof(Attribute)))
        {
            _attributeLevels[attribute] = new(1);
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
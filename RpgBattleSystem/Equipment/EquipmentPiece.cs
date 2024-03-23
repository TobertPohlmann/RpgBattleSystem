using RpgBattleSystem.Enums;
using Attribute = RpgBattleSystem.Enums.Attribute;
namespace RpgBattleSystem.Equipment;

public class EquipmentPiece
{
    public readonly string Name;
    private Dictionary<Status,int> _boni = new ();
    private Dictionary<Attribute, int> _requirements = new();

    public EquipmentPiece(string name)
    {
        Name = name;
        foreach (Status status in Enum.GetValues(typeof(Status)))
        {
            _boni[status] = 0;
        }
        
        foreach (Attribute attribute in Enum.GetValues(typeof(Attribute)))
        {
            _requirements[attribute] = 0;
        }
    }

    public EquipmentPiece WithBonus(Status status, int value)
    {
        _boni[status] = value;
        return this;
    }
    
    public EquipmentPiece WithRequirement(Attribute attribute, int value)
    {
        _requirements[attribute] = value;
        return this;
    }

    public int GetBonusFor(Status status)
    {
        return _boni[status];
    }
    
    public int GetRequirementFor(Attribute attribute)
    {
        return _requirements[attribute];
    }
}
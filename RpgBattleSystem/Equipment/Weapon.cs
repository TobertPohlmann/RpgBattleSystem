
using RpgBattleSystem.Skills;
using Attribute = RpgBattleSystem.Enums.Attribute;

namespace RpgBattleSystem.Equipment;

public class Weapon : EquipmentPiece
{
    public int BaseAttack;
    public List<Skill> Skills = new();
    internal Dictionary<Attribute, double> _scaling = new();

    public Weapon(string name, int baseAttack) : base(name)
    {
        BaseAttack = baseAttack;
        foreach (Attribute attribute in Enum.GetValues(typeof(Attribute)))
        {
            _scaling[attribute] = 0;
        }
    }
}
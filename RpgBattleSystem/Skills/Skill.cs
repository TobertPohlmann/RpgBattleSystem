using RpgBattleSystem.Enums;
using RpgBattleSystem.Skills.Effects;

namespace RpgBattleSystem.Skills;

public class Skill
{
    public string Name { get; } = "Skill";
    public List<Effect> Effects { get; } = new();

    public Skill(string name)
    {
        Name = name;
    }
    
    public Skill WithEffect(Effect effect)
    {
        Effects.Add(effect);
        return this;
    }
}
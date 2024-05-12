using RpgBattleSystem.Enums;
using RpgBattleSystem.Skills.Effects;

namespace RpgBattleSystem.Skills;

public class Skill
{
    public string Name { get; } = "Skill";
    public List<Effect> Effects { get; } = new();
    public TargetScope Scope { get; private set; } = TargetScope.SingleOpponent;
    public Skill(string name)
    {
        Name = name;
    }
    
    public Skill WithEffect(Effect effect)
    {
        Effects.Add(effect);
        return this;
    }

    public Skill WithScope(TargetScope scope)
    {
        Scope = scope;
        return this;
    }
}
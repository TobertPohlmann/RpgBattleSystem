using RpgBattleSystem.Characters;
using RpgBattleSystem.Enums;

namespace RpgBattleSystem.Skills.Effects;

public abstract class Effect
{
    internal Character? User;
    internal List<Character>? Targets;
    public EffectDirection Direction;

    public Effect(EffectDirection direction)
    {
        Direction = direction;
    }

    public void Apply()
    {
        if (Targets == null || User == null)
        {
            throw new Exception("Cannot apply effect without target and user being defined.");
        }
        List<Character> recipients = (Direction == EffectDirection.Target) ? Targets : new List<Character> {User};
        ApplyTo(recipients);
    }

    public Effect WithTargets(List<Character> targets)
    {
        Targets = targets;
        return this;
    }
    
    public Effect WithUser(Character user)
    {
        User = user;
        return this;
    }

    internal abstract void ApplyTo(List<Character> characters);

}
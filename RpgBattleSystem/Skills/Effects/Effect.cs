using RpgBattleSystem.Characters;
using RpgBattleSystem.Enums;

namespace RpgBattleSystem.Skills.Effects;

public abstract class Effect
{
    internal Character? User;
    internal Character? Target;
    public EffectDirection Direction;

    public Effect(EffectDirection direction)
    {
        Direction = direction;
    }

    public void Apply()
    {
        if (Target == null || User == null)
        {
            throw new Exception("Cannot apply effect without target and user being defined.");
        }
        Character recipient = (Direction == EffectDirection.Target) ? Target : User;
        ApplyTo(recipient);
    }

    public Effect WithTarget(Character target)
    {
        Target = target;
        return this;
    }
    
    public Effect WithUser(Character user)
    {
        User = user;
        return this;
    }

    internal abstract void ApplyTo(Character character);

}
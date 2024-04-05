using RpgBattleSystem.Characters;
using RpgBattleSystem.Characters.StatusValues;
using RpgBattleSystem.Enums;

namespace RpgBattleSystem.Skills.Effects;

public class StanceShift : Effect
{
    public readonly BoundedValue Value = new (0,-200,200);

    public StanceShift(int value, EffectDirection direction = EffectDirection.Target) : base(direction)
    {
        Value.SetValue(value);
    }

    internal override void ApplyTo(Character recipient)
    {
        recipient.Stance+= Value;
    }
}
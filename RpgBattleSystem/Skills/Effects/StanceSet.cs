using RpgBattleSystem.Characters;
using RpgBattleSystem.Characters.StatusValues;
using RpgBattleSystem.Enums;

namespace RpgBattleSystem.Skills.Effects;

public class StanceSet : Effect
{
    private BoundedValue _value = new (0,-100,100);

    public StanceSet(int value, EffectDirection direction) : base(direction)
    {
        _value.SetValue(value);
    }

    internal override void ApplyTo(Character recipient)
    {
        recipient.Stance = _value;
    }
}
using RpgBattleSystem.Characters;
using RpgBattleSystem.Characters.StatusValues;
using RpgBattleSystem.Enums;

namespace RpgBattleSystem.Skills.Effects;

public class StanceSet : Effect
{
    public BoundedValue Value = new (0,-100,100);

    public StanceSet(int value, EffectDirection direction) : base(direction)
    {
        Value.SetValue(value);
    }

    internal override void ApplyTo(List<Character> recipients)
    {
        foreach (var recipient in recipients)
        {
            recipient.Stance = Value;
        }
    }
}
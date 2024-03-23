using RpgBattleSystem.Characters;
using RpgBattleSystem.Enums;

namespace RpgBattleSystem.Skills.Effects;

public class BuffEffect : Effect
{
    private Buff _buff;

    public BuffEffect(Buff buff, EffectDirection direction) : base(direction)
    {
        _buff = buff;
    }

    internal override void ApplyTo(Character recipient)
    {
        recipient.Buffs.AddBuff(_buff);
    }
}
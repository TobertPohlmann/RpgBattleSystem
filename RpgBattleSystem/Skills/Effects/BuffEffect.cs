using RpgBattleSystem.Characters;
using RpgBattleSystem.Enums;

namespace RpgBattleSystem.Skills.Effects;

public class BuffEffect : Effect
{
    private Buff _buff;
    private int _duration = 0;

    public BuffEffect(Buff buff, int duration, EffectDirection direction) : base(direction)
    {
        _buff = buff;
        _duration = duration;
    }

    internal override void ApplyTo(Character recipient)
    {
        recipient.Buffs.AddBuff(_buff);
    }
}
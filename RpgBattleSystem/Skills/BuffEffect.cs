using RpgBattleSystem.Characters;

namespace RpgBattleSystem.Skills;

public class BuffEffect : Effect
{
    private Character _target;
    private Buff _buff;

    public BuffEffect(Character target, Buff buff)
    {
        _target = target;
        _buff = buff;
    }

    public void Apply()
    {
        _target.Buffs.AddBuff(_buff);
    }
}
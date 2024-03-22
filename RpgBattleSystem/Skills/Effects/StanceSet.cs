using RpgBattleSystem.Characters;

namespace RpgBattleSystem.Skills;

public class StanceSet : IEffect
{
    private Character _target;
    private BoundedValue _value = new (0,-100,100);

    public StanceSet(Character target, int value)
    {
        _target = target;
        _value.SetValue(value);
    }

    public void Apply()
    {
        _target.Stance = _value;
    }
}
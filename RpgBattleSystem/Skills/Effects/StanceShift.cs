using RpgBattleSystem.Characters;

namespace RpgBattleSystem.Skills;

public class StanceShift : IEffect
{
    private Character _target;
    private BoundedValue _value = new (0,-200,200);

    public StanceShift(Character target, int value)
    {
        _target = target;
        _value.SetValue(value);
    }

    public void Apply()
    {
        _target.Stance+= _value;
    }
}
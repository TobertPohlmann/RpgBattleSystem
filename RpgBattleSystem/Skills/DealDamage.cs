using RpgBattleSystem.Characters;

namespace RpgBattleSystem.Skills;

public class DealDamage : Effect
{
    
    
    
    private Character _target;
    private AttackType _type;
    private int _attackValue;

    public DealDamage(Character target, int attackValue)
    {
        _target = target;
        _attackValue = attackValue;
    }

    public void Apply()
    {
        int damage = CalculateDamage();
        _target.Health -= damage;
    }

    private int CalculateDamage()
    {
        int defense = _target.StatusValue(_type.Defense());
        return _attackValue - defense;
    }
    
    
}
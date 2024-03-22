using RpgBattleSystem.Characters;

namespace RpgBattleSystem.Skills;

public class DealDamage : IEffect
{
    private Character _target;
    private AttackType _type;
    private int _attackValue;

    public DealDamage(Character target, int attackValue, AttackType type)
    {
        _target = target;
        _attackValue = attackValue;
        _type = type;
    }

    public void Apply()
    {
        int damage = CalculateDamage();
        _target.Health -= damage;
    }

    private int CalculateDamage()
    {
        Status status = _type.Defense();
        int totalDefense = _target.StatusValue(status);
        int baseDefense = _target.Base.GetStatusValueFor(status);
        int equipDefense = _target.Equipment.GetTotalBonusFor(status);

        switch (_type)
        {
            case AttackType.Cut: return CalculateCutDamage(baseDefense,equipDefense);
            case AttackType.Pierce: return CalculatePierceDamage(baseDefense,equipDefense);
            case AttackType.Strike: return CalculateStrikeDamage(totalDefense);
        }
        throw new Exception("Attack type "+_type+" is not registered.");
    }

    private int CalculatePierceDamage(int baseDefense, int equipDefense)
    {
        int effectiveDefense = _target.Buffs.GetModifiedValue((int)((2 * baseDefense + equipDefense) / 3.0), Status.PierceDefense);
        return CalculateDamage(effectiveDefense);
    }
    
    private int CalculateCutDamage(int baseDefense, int equipDefense)
    {
        int effectiveDefense = _target.Buffs.GetModifiedValue((int)((baseDefense + 2*equipDefense) / 3.0), Status.CutDefense);
        return CalculateDamage(effectiveDefense);
    }
    
    private int CalculateStrikeDamage(int totalDefense)
    {
        return CalculateDamage(totalDefense);
    }

    private int CalculateDamage(double effectiveDefense)
    {
        double damage;
        if (_attackValue >= effectiveDefense)
        {
            damage = 2 * _attackValue - effectiveDefense;
        }
        else
        {
            damage = _attackValue * _attackValue / effectiveDefense;
        }
        return (int)damage;
    }
    
}
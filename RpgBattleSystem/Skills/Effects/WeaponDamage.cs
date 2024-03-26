using RpgBattleSystem.Characters;
using RpgBattleSystem.Enums;
using RpgBattleSystem.Equipment;

namespace RpgBattleSystem.Skills.Effects;

public class WeaponDamage : Effect
{
    private AttackType _type;
    private Weapon _weapon;
    private double _multiplier;

    public WeaponDamage(AttackType type, Weapon weapon, double multiplier = 1, EffectDirection direction = EffectDirection.Target) : base(direction) 
    {
        _type = type;
        _multiplier = multiplier;
        _weapon = weapon;
    }
    
    internal override void ApplyTo(Character recipient)
    {
        int damage = (int)(CalculateDamage(recipient)*_multiplier);
        recipient.Health -= damage;
    }

    private double CalculateDamage(Character recipient)
    {
        int attackValue = User.GetAttackFor(_weapon);

        Status defenseStatus = _type.Defense();
        int totalDefense = recipient.StatusValue(defenseStatus);
        int baseDefense = recipient.Base.GetStatusValueFor(defenseStatus);
        int equipDefense = recipient.Equipment.GetTotalBonusFor(defenseStatus);
        
        double effectiveDefense;
        switch (_type)
        {
            case AttackType.Cut: effectiveDefense = recipient.Buffs.GetModifiedValue((int)((baseDefense + 2*equipDefense) / 3.0), Status.CutDefense);
                break;
            case AttackType.Pierce: effectiveDefense = recipient.Buffs.GetModifiedValue((int)((2 * baseDefense + equipDefense) / 3.0), Status.PierceDefense);
                break;
            case AttackType.Strike: effectiveDefense = totalDefense;
                break;
            default: throw new Exception("Attack type "+_type+" is not registered.");
        }

        return CalculateDamage(attackValue,effectiveDefense);
    }

    private double CalculateDamage(int attackValue, double effectiveDefense)
    {
        double damage;
        if (attackValue >= effectiveDefense)
        {
            damage = 2 * attackValue - effectiveDefense;
        }
        else
        {
            damage = attackValue * attackValue / effectiveDefense;
        }
        return damage;
    }
}
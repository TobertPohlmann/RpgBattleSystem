using RpgBattleSystem.Characters;
using RpgBattleSystem.Enums;
using RpgBattleSystem.Equipment;

namespace RpgBattleSystem.Skills.Effects;

public class WeaponDamage : Effect
{
    public AttackType AttackType;
    private Weapon _weapon;
    public double Multiplier;

    public WeaponDamage(AttackType attackType, Weapon weapon, double multiplier = 1, EffectDirection direction = EffectDirection.Target) : base(direction) 
    {
        AttackType = attackType;
        Multiplier = multiplier;
        _weapon = weapon;
    }
    
    internal override void ApplyTo(List<Character> recipients)
    {
        foreach (var recipient in recipients)
        {
            int damage = (int)(CalculateDamage(recipient)*Multiplier);
            recipient.Health -= damage;
        }
    }

    private double CalculateDamage(Character recipient)
    {
        int attackValue = User.GetAttackFor(_weapon);

        Status defenseStatus = AttackType.Defense();
        int totalDefense = recipient.StatusValue(defenseStatus);
        int baseDefense = recipient.Base.GetStatusValueFor(defenseStatus);
        int equipDefense = recipient.Equipment.GetTotalBonusFor(defenseStatus);
        
        double effectiveDefense;
        switch (AttackType)
        {
            case AttackType.Cut: effectiveDefense = recipient.Buffs.GetModifiedValue((int)((baseDefense + 2*equipDefense) / 3.0), Status.CutDefense);
                break;
            case AttackType.Pierce: effectiveDefense = recipient.Buffs.GetModifiedValue((int)((2 * baseDefense + equipDefense) / 3.0), Status.PierceDefense);
                break;
            case AttackType.Strike: effectiveDefense = totalDefense;
                break;
            default: throw new Exception("Attack type "+AttackType+" is not registered.");
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
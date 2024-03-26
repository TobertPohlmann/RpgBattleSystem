using RpgBattleSystem.Enums;
using RpgBattleSystem.Skills;
using RpgBattleSystem.Skills.Effects;
using Attribute = RpgBattleSystem.Enums.Attribute;

namespace RpgBattleSystem.Equipment.Weapons;

public class Langschwert : Weapon
{
    public Langschwert() : base("Langschwert", 25)
    {
        Skills.Add(new Skill("Schwertschnitt")
            .WithEffect(new WeaponDamage(AttackType.Cut,this,1,EffectDirection.Target))
            .WithEffect(new StanceShift(-15,EffectDirection.Target))
            .WithEffect(new StanceShift(15,EffectDirection.User))
        );

        Skills.Add(new Skill("Schwertstoß")
            .WithEffect(new WeaponDamage(AttackType.Pierce, this, 1, EffectDirection.Target))
            .WithEffect(new StanceShift(-80, EffectDirection.User))
        );
        _scaling[Attribute.Strength] = 0.5;
        _scaling[Attribute.Dexterity] = 1.5;
    }
    
}
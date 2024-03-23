using RpgBattleSystem.Characters;
using RpgBattleSystem.Enums;
using RpgBattleSystem.Skills;
using RpgBattleSystem.Skills.Effects;
using Attribute = RpgBattleSystem.Enums.Attribute;

namespace RpgBattleSystem.Equipment.Weapons;

public class Kurzspeer : Weapon
{
    public Kurzspeer() : base("Kurzspeer", 20)
    {
        Skills.Add(new Skill("Speerstoß")
            .WithEffect(new StanceShift(30,EffectDirection.User))
            .WithEffect(new WeaponDamage(AttackType.Pierce,1,EffectDirection.Target))
            .WithEffect(new StanceShift(30,EffectDirection.Target))
            );

        Skills.Add(new Skill("Speerwurf")
            .WithEffect(new WeaponDamage(AttackType.Pierce, 0.8, EffectDirection.Target))
            .WithEffect(new StanceShift(-80, EffectDirection.User))
        );
        _scaling[Attribute.Dexterity] = 1.5;
    }
}
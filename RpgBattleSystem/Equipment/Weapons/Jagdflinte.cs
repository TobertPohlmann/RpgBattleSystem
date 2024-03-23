using RpgBattleSystem.Enums;
using RpgBattleSystem.Skills;
using RpgBattleSystem.Skills.Effects;
using Attribute = RpgBattleSystem.Enums.Attribute;

namespace RpgBattleSystem.Equipment.Weapons;

public class Jagdflinte : Weapon
{
    public Jagdflinte() : base("Jagdflinte", 15)
    {
        Skills.Add(new Skill("Aufgesetzter Schuss")
            .WithEffect(new WeaponDamage(AttackType.Strike,0.7,EffectDirection.Target))
            .WithEffect(new WeaponDamage(AttackType.Pierce,0.7,EffectDirection.Target))
            .WithEffect(new StanceShift(-80,EffectDirection.Target)));

        Skills.Add(new Skill("Deckungsschuss")
            .WithEffect(new WeaponDamage(AttackType.Pierce, 1, EffectDirection.Target))
            .WithEffect(new StanceSet(-50, EffectDirection.User))
        );
        
        _scaling[Attribute.Focus] = 1.5;
    }
}
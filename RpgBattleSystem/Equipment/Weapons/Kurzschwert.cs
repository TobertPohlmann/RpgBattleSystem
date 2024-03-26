using RpgBattleSystem.Characters;
using RpgBattleSystem.Enums;
using RpgBattleSystem.Skills;
using RpgBattleSystem.Skills.Effects;
using Attribute = RpgBattleSystem.Enums.Attribute;

namespace RpgBattleSystem.Equipment.Weapons;

public class Kurzschwert : Weapon
{
    public Kurzschwert() : base("Kurzschwert", 20)
    {
        var swordStrike = new Skill("Schwerthieb")
            .WithEffect(new WeaponDamage(AttackType.Strike, this,1,EffectDirection.Target))
            .WithEffect(new StanceShift(-25,EffectDirection.Target))
            .WithEffect(new StanceShift(15,EffectDirection.User));
        
        var swiftSwing = new Skill("Flinker Schwung")
            .WithEffect(new WeaponDamage(AttackType.Strike, this,0.8,EffectDirection.Target))
            .WithEffect(new StanceShift(-50,EffectDirection.User));
        
        Skills.Add(swordStrike);
        Skills.Add(swiftSwing);
        
        _scaling[Attribute.Strength] = 1.0;
        _scaling[Attribute.Dexterity] = 0.7;
    }
    
}
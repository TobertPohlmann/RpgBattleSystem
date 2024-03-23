using RpgBattleSystem.Characters;
using RpgBattleSystem.Enums;
using RpgBattleSystem.Skills;
using RpgBattleSystem.Skills.Effects;
using Attribute = RpgBattleSystem.Enums.Attribute;

namespace RpgBattleSystem.Equipment.Weapons;

public class ShortSword : Weapon
{
    public ShortSword() : base("Short sword", 20)
    {
        var swordSwing = new Skill("Sword swing")
            .WithEffect(new WeaponDamage(AttackType.Strike))
            .WithEffect(new StanceShift(-20));
        

        _scaling[Attribute.Strength] = 1.0;
        _scaling[Attribute.Dexterity] = 0.7;
        
        
        
    }
    
}
using RpgBattleSystem.Equipment;

namespace RpgBattleSystem.Characters;

public record CharacterEquipment
{
    public Weapon? Weapon = null;
    public Helmet? Helmet = null;
    public ChestArmor? Chest = null;
    public LegArmor? Legs = null;
    public Accessory? Accessory1 = null;
    public Accessory? Accessory2 = null;
    public Accessory? Accessory3 = null;

    public int GetTotalBonusFor(Status status)
    {
        return Weapon.GetBonusFor(status) +
               Helmet.GetBonusFor(status) +
               Chest.GetBonusFor(status) +
               Legs.GetBonusFor(status) +
               Accessory1.GetBonusFor(status) +
               Accessory2.GetBonusFor(status) +
               Accessory3.GetBonusFor(status);
    }
}
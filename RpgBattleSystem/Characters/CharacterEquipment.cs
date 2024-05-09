using RpgBattleSystem.Enums;
using RpgBattleSystem.Equipment;

namespace RpgBattleSystem.Characters;

public record CharacterEquipment
{
    public Weapon? Weapon1 = null;
    public Weapon? Weapon2 = null;
    public Weapon? Weapon3 = null;
    public Helmet? Helmet = null;
    public ChestArmor? Chest = null;
    public LegArmor? Legs = null;
    public Accessory? Accessory1 = null;
    public Accessory? Accessory2 = null;
    public Accessory? Accessory3 = null;

    public bool HasWeaponEquipped()
    {
        return GetNumberOfWeapons() > 0;
    }

    public int GetNumberOfWeapons()
    {
        int weaponCount = Weapon1 != null ? 1 : 0;
        weaponCount +=  Weapon2 != null ? 1 : 0;
        weaponCount +=  Weapon3 != null ? 1 : 0;
        return weaponCount;
    }
    
    public int GetTotalBonusFor(Status status)
    {
        return GetBonusOf(Helmet, status) +
               GetBonusOf(Chest, status) +
               GetBonusOf(Legs, status) +
               GetBonusOf(Accessory1, status) +
               GetBonusOf(Accessory2, status) +
               GetBonusOf(Accessory3, status);
    }

    private int GetBonusOf(EquipmentPiece? equipment, Status status)
    {
        return equipment != null ? equipment.GetBonusFor(status) : 0;
    }
}
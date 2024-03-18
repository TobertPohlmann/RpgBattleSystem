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
        return GetBonusOf(Weapon, status) +
               GetBonusOf(Helmet, status) +
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
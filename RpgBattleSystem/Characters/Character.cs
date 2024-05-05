using RpgBattleSystem.Characters.StatusValues;
using RpgBattleSystem.Enums;
using RpgBattleSystem.Equipment;
using Attribute = RpgBattleSystem.Enums.Attribute;

namespace RpgBattleSystem.Characters;

public class Character
{
    public CharacterBase Base { get; }
    public int Health;
    public BoundedValue Stance = new (0, -100, 100);
    public CharacterEquipment Equipment { get; } = new();
    public CharacterBuffs Buffs { get; } = new();
    public List<StatusAffliction> Afflictions = new();
    public Character(string name, Sex sex = Sex.Male, Heritage heritage = Heritage.Preußen)
    {
        Base = new(name,sex,heritage);
        Health = Base.GetStatusValueFor(Status.MaxHealth);
    }
    
    public int StrikeDefense
    {
        get { return StatusValue(Status.StrikeDefense); }
    }
    
    public int StrikeForce {
        get { return StatusValue(Status.StrikeForce); }
    }
    
    public int Technique
    {
        get { return StatusValue(Status.Technique); }
    }
    
    public int Precision
    {
        get { return StatusValue(Status.Precision); }
    }
    
    public int CutDefense
    {
        get { return StatusValue(Status.CutDefense); }
    }
    
    public int PierceDefense
    {
        get { return StatusValue(Status.PierceDefense); }
    }
    
    public int PoisonResistance
    {
        get { return StatusValue(Status.PoisonResistance); }
    }
    
    public int HeatResistance
    {
        get { return StatusValue(Status.HeatResistance); }
    }
    
    public int ColdResistance
    {
        get { return StatusValue(Status.ColdResistance); }
    }

    public int EquipLoad
    {
        get { return StatusValue(Status.EquipLoad); }
    }
    
    public int Speed
    {
        get { return StatusValue(Status.Speed); }
    }

    public int StatusValue(Status status)
    {
        int baseValue = Base.GetStatusValueFor(status);
        int equipmentValue = Equipment.GetTotalBonusFor(status);
        return Buffs.GetModifiedValue(baseValue + equipmentValue, status);
    }
    

    public int GetAttackFor(Weapon weapon)
    {
        double totalAttack = weapon.BaseAttack;
        foreach (Attribute attribute in Enum.GetValues(typeof(Attribute)))
        {
            totalAttack += weapon.GetScalingFor(attribute)*Base.GetLevelFor(attribute);
        }
        return (int)totalAttack;
    }
}
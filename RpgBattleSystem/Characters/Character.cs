namespace RpgBattleSystem.Characters;

public class Character
{
    public CharacterBase Base { get; }
    public int Health;
    public CharacterEquipment Equipment { get; } = new();
    public CharacterBuffs Buffs { get; } = new();
    public List<StatusAffliction> Afflictions = new();
    public Character(string name)
    {
        Base = new(name);
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
}
namespace RpgBattleSystem.Characters;

public class Character
{
    public CharacterBase Base { get; }
    public int Health;
    public CharacterEquipment Equipment { get; }

    public int StrikeDefense
    {
        get { return GetTotalStatusValueFor(Status.StrikeDefense); }
    }

    private int GetTotalStatusValueFor(Status status)
    {
        return Base.GetStatusValueFor(Status.StrikeDefense);
    }
    
    public Character(string name)
    {
        Base = new(name);
        Health = Base.GetStatusValueFor(Status.MaxHealth);
    }
    
}
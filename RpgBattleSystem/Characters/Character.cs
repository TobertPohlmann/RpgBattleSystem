namespace RpgBattleSystem.Characters;

public class Character
{
    public CharacterBase Base { get; }
    public int Health;
    public CharacterEquipment Equipment { get; }

    public int PhysicalDefense
    {
        get { return 0; }
    }
    
    public Character(string name)
    {
        Base = new(name);

    }
    
}
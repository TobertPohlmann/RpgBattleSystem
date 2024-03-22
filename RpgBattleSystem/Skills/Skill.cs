namespace RpgBattleSystem.Skills;

public class Skill
{
    public string Name { get; } = "Skill";
    public List<IEffect> Effects { get; } = new();

    public Skill(string name)
    {
        Name = name;
    }
    
    public Skill WithEffect(IEffect effect)
    {
        Effects.Add(effect);
        return this;
    }
}
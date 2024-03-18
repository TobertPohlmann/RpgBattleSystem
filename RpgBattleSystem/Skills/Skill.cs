namespace RpgBattleSystem.Skills;

public class Skill
{
    private List<Effect> _effects = new();

    public Skill WithEffect(Effect effect)
    {
        _effects.Add(effect);
        return this;
    }
}
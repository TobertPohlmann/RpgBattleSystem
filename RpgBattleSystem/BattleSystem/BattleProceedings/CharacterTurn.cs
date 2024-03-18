using RpgBattleSystem.Characters;
using RpgBattleSystem.Skills;

namespace RpgBattleSystem.BattleSystem.BattleProceedings;

public class CharacterTurn
{
    private Character _actor;
    private Skill _move;
    private List<Character> _targets;

    public CharacterTurn(Character actor, Skill move, List<Character> targets)
    {
        _actor = actor;
        _move = move;
        _targets = targets;
    }
}
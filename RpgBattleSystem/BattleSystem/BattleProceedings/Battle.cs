using RpgBattleSystem.Characters;

namespace RpgBattleSystem.BattleSystem.BattleProceedings;

public class Battle
{
    private List<Character> _participants;

    public Battle(List<Character> participants)
    {
        _participants = participants;
    }
}
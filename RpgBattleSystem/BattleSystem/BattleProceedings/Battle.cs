using RpgBattleSystem.Characters;

namespace RpgBattleSystem.BattleSystem.BattleProceedings;

public class Battle
{
    public List<Character> HeroParty { get; }
    public List<Character> EnemyParty { get; }
    private List<BattleTurn> _battleTurns;

    public Battle(List<Character> heroParty, List <Character> enemyParty)
    {
        HeroParty = heroParty;
        EnemyParty = enemyParty;
    }
}
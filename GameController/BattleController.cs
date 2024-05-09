using ConsoleView.BattleScreen;
using RpgBattleSystem.BattleSystem.BattleProceedings;
using RpgBattleSystem.Characters;

namespace GameController;

public class BattleController
{
    private Battle _battle;
    private BattleScreen _screen;

    public BattleController(List<Character> heroParty, List<Character> enemyParty)
    {
        _battle = new Battle(heroParty,enemyParty);
        _screen = new BattleScreen(_battle);
        _screen.Draw();
    }

    public void Start()
    {
        GetTurnInput(_battle.HeroParty[0]);
    }

    private CharacterTurn GetTurnInput(Character character)
    {
        var turnInputRequest = new TurnInputRequest(character, _screen);
        return turnInputRequest.GetTurnInput();
    }

}
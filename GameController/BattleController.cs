using ConsoleView.BattleScreen;
using RpgBattleSystem.BattleSystem.BattleProceedings;
using RpgBattleSystem.Characters;
using Spectre.Console;

namespace GameController;

public class BattleController
{
    private Battle _battle;
    private BattleScreen _screen;

    public BattleController(List<Character> heroParty, List<Character> enemyParty)
    {
        _battle = new Battle(heroParty,enemyParty);
        _screen = new BattleScreen(_battle);
    }

    public void Start()
    {
        AnsiConsole.Live(_screen.Renderable)
            .Start(ctx =>
            {
                ctx.Refresh();
                GetTurnInput(_battle.HeroParty[0],ctx);
            });
    }

    private CharacterTurn GetTurnInput(Character character, LiveDisplayContext ctx)
    {
        var turnInputRequest = new TurnInputRequest(character, _screen, ctx);
        return turnInputRequest.GetTurnInput();
    }

}
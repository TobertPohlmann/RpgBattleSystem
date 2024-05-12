using ConsoleView.CharacterPanels;
using ConsoleView.CommonElements;
using ConsoleView.Messages;
using RpgBattleSystem.BattleSystem.BattleProceedings;
using RpgBattleSystem.Characters;
using Spectre.Console;

namespace ConsoleView.BattleScreen;

public class BattleScreen : RenderableWrapper
{
    public Battle Battle;
    private PartyLayout _heroLayout;
    private PartyLayout _enemyLayout;
    private MessageBox _messageBox;
    public Layout Renderable;

    
    public BattleScreen(Battle battle)
    {
        Battle = battle;
        CreateLayout();
        Render();
    }

    public void Draw()
    {
        Render();
        AnsiConsole.Write(Renderable);
    }

    public CharacterPanel GetPanelFor(Character character)
    {
        if (_heroLayout.CharacterPanels.ContainsKey(character))
        {
            return _heroLayout.CharacterPanels[character];
        }
        
        if (_enemyLayout.CharacterPanels.ContainsKey(character))
        {
            return _enemyLayout.CharacterPanels[character];
        }
        throw new Exception("There is no panel registered for character " + character.Base.Name);
    }

    public void WriteMessage(string text)
    {
        _messageBox.Message = text;
    }
    
    private void CreateLayout()
    {
        _heroLayout = new PartyLayout("HeroRow", Battle.HeroParty);
        _enemyLayout = new PartyLayout("EnemyRow", Battle.EnemyParty);
        _messageBox = new MessageBox("MessageRow",BattleBeginsMessage());
        
        Renderable = new Layout("Root")
            .SplitRows(_enemyLayout.Layout,
                _messageBox.Layout,
                _heroLayout.Layout);
    }


    public override void Render()
    {
        _heroLayout.UpdateLayout(SubPanelType.Standard);
        _enemyLayout.UpdateLayout(SubPanelType.Standard);
        _messageBox.UpdateLayout();
    }

    private string BattleBeginsMessage()
    {
        string message = "A ";
        string conjunction;
        for (int i = 0; i < Battle.EnemyParty.Count; i++)
        {
            conjunction = (Battle.EnemyParty.Count - i) switch
            {
                1 => " ",
                2 => " and a ",
                _ => ", a "
            };
            message += "[" + ColorRegistry.EnemyColor.ToMarkup() + "]" + Battle.EnemyParty[i].Base.Name + "[/]" +
                        conjunction;
        }

        message += "attack".ConjugateForNumerus(Battle.EnemyParty.Count) + ".\n";

        string conjunction2;
        for (int i = 0; i < Battle.HeroParty.Count; i++)
        {
            conjunction2 = (Battle.HeroParty.Count - i) switch
            {
                1 => " ",
                2 => " and ",
                _ => ", "
            };
            message += "[" + ColorRegistry.HeroColor.ToMarkup() + "]" + Battle.HeroParty[i].Base.Name + "[/]" +
                        conjunction2;
        }

        message += "draw".ConjugateForNumerus(Battle.HeroParty.Count) + " "
                    + Pronoun.ForParty(Battle.HeroParty).With(Casus.Genitive).Get()
                    + " weapon".DeclinateForNumerus(Battle.HeroParty.Count())
                    +".";
        return message;
    }
}
using ConsoleView.CharacterPanels;
using ConsoleView.Messages;
using RpgBattleSystem.BattleSystem.BattleProceedings;
using RpgBattleSystem.Characters;
using Spectre.Console;

namespace ConsoleView.BattleScreen;

public class BattleScreen
{
    private Battle _battle;
    private PartyLayout _heroLayout;
    private PartyLayout _enemyLayout;
    private MessageBox _messageBox;
    private Layout _layout;
    
    public BattleScreen(Battle battle)
    {
        _battle = battle;
        CreateLayout();
    }

    public void Draw()
    {
        Update();
        AnsiConsole.Write("\n");
        AnsiConsole.Write(_layout);
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
        _messageBox = new MessageBox("MessageRow", text);
    }
    
    private void CreateLayout()
    {
        _heroLayout = new PartyLayout("HeroRow", _battle.HeroParty);
        _enemyLayout = new PartyLayout("EnemyRow", _battle.EnemyParty);
        WriteMessage(BattleBeginsMessage());
        
        _layout = new Layout("Root")
            .SplitRows(_enemyLayout.Layout,
                _messageBox.Layout,
                _heroLayout.Layout);
    }


    private void Update()
    {
        _heroLayout.UpdateLayout(SubPanelType.Standard);
        _enemyLayout.UpdateLayout(SubPanelType.Standard);
        _messageBox.UpdateLayout();
    }

    private string BattleBeginsMessage()
    {
        string message = "A ";
        string conjunction;
        for (int i = 0; i < _battle.EnemyParty.Count; i++)
        {
            conjunction = (_battle.EnemyParty.Count - i) switch
            {
                1 => " ",
                2 => " and a ",
                _ => ", a "
            };
            message += "[" + ColorRegistry.EnemyColor.ToMarkup() + "]" + _battle.EnemyParty[i].Base.Name + "[/]" +
                        conjunction;
        }

        message += "attack".ConjugateForNumerus(_battle.EnemyParty.Count) + ".\n";

        string conjunction2;
        for (int i = 0; i < _battle.HeroParty.Count; i++)
        {
            conjunction2 = (_battle.HeroParty.Count - i) switch
            {
                1 => " ",
                2 => " and ",
                _ => ", "
            };
            message += "[" + ColorRegistry.HeroColor.ToMarkup() + "]" + _battle.HeroParty[i].Base.Name + "[/]" +
                        conjunction2;
        }

        message += "draw".ConjugateForNumerus(_battle.HeroParty.Count) + " "
                    + Pronoun.ForParty(_battle.HeroParty).With(Casus.Genitive).Get()
                    + " weapon".DeclinateForNumerus(_battle.HeroParty.Count())
                    +".";
        return message;
    }
}
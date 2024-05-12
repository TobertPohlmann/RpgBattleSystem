using ConsoleView.BattleScreen;
using ConsoleView.CharacterPanels;
using ConsoleView.CharacterPanels.StandardPanel;
using RpgBattleSystem.BattleSystem.BattleProceedings;
using RpgBattleSystem.Characters;
using RpgBattleSystem.Equipment.Weapons;
using Spectre.Console;

public static class Program
{
    public static void Main(string[] args)
    {
//        AnsiConsole.Markup("[underline red]Hello[/] World!");

        Character hans = new("Hans");
        hans.Equipment.Weapon1 = new Kurzschwert();
        hans.Equipment.Weapon2 = new Jagdflinte();
        hans.Equipment.Weapon3 = new Kurzspeer();
        
        Character erika = new("Erika");
        Character gegner = new("Gegner");

        List<Character> heros = new List<Character>();
        heros.Add(hans);
        heros.Add(erika);

        List<Character> enemies = new List<Character>();
        enemies.Add(gegner);

        Battle battle = new Battle(heros,enemies);

        var heroLayout = new PartyLayout("heros", heros);
        heroLayout.UpdateLayout(SubPanelType.Standard);

        CharacterPanel cPanel = new CharacterPanel(hans);
        //AnsiConsole.Write(cPanel.GetSubPanelFor(SubPanelType.Standard));

        //AnsiConsole.Write(heroLayout.Layout);

        BattleScreen battleScreen = new BattleScreen(battle);

        battleScreen.Draw();

        //CharacterStandardPanel standardPanel = new(hans);
        //CharacterStatPanel statPanel = new(hans);
        //standardPanel.Draw();
        //basePanel.Draw();
    }
}

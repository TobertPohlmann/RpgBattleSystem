using ConsoleView.CharacterPanels;
using RpgBattleSystem.Characters;
using RpgBattleSystem.Equipment.Weapons;
using Spectre.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        AnsiConsole.Markup("[underline red]Hello[/] World!");

        Character hans = new("Hans");
        hans.Equipment.Weapon1 = new Kurzschwert();
        CharacterHealthPanel healthPanel = new(hans);
        CharacterBasePanel basePanel = new(hans);
        healthPanel.Draw();
        basePanel.Draw();

    }
}

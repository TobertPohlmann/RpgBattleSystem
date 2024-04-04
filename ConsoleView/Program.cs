using ConsoleView.CharacterPanels;
using RpgBattleSystem.Characters;
using Spectre.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        AnsiConsole.Markup("[underline red]Hello[/] World!");

        Character hans = new("Hans");
        CharacterPanel hansPanel = new(hans);
        Panel panel = new Panel("Huhn");
        panel.Header = new PanelHeader("HUHN");
        panel.Expand = true;
        hansPanel.Draw();

    }
}

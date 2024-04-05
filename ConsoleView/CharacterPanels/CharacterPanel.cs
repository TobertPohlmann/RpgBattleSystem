using RpgBattleSystem.Characters;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ConsoleView.CharacterPanels;

public abstract class CharacterPanel
{
    private protected readonly Style HeadlineColor = new Style(foreground:Color.Grey100,decoration:Decoration.Underline);
    private protected const int Width = 40;
    private protected const int Height = 14;
    private protected Character Character;

    public CharacterPanel(Character character)
    {
        Character = character;
    }
    
    public void Draw()
    {
        AnsiConsole.Write("\n");
        AnsiConsole.Write(CreatePanel());
    }
    
    private Panel CreatePanel()
    {
        var content = CreateContent();
        var panel = new Panel(content);
        panel.Header = CreatePanelHeader();
        panel.Width = Width;
        panel.Height = Height;
        return panel;
    }
    
    private PanelHeader CreatePanelHeader()
    {
        string name = "- "+Character.Base.Name+" -";
        string level = "- Lv. " + Character.Base.GetLevel()+" -";
        string header = (name.PadRight((Width+name.Length) / 2 - level.Length-2,'─')+level).PadLeft(Width-5,'─');
        return new PanelHeader(header);
    }

    private protected abstract Renderable CreateContent();
}
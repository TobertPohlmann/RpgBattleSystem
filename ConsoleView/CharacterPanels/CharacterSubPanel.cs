using ConsoleView.CommonElements;
using RpgBattleSystem.Characters;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ConsoleView.CharacterPanels;

public abstract class CharacterSubPanel : SelectableElement
{
    public Panel Renderable;
    private protected const int Width = 40;
    public static int Height = 9;
    private protected Character Character;

    public CharacterSubPanel(Character character)
    {
        Character = character;
        //Select(true);
    }
    
    public void Draw()
    {
        Render();
        AnsiConsole.Write("\n");
        AnsiConsole.Write(Renderable);
    }
    
    public override void Render()
    {
        var content = CreateContent();
        Renderable = new Panel(content);
        Renderable.Header = CreatePanelHeader();
        Renderable.Width = Width;
        Renderable.Height = Height;
        if (_selected)
        {
            Renderable.BorderStyle = ColorRegistry.SelectStyle;
        }
    }
    
    private PanelHeader CreatePanelHeader()
    {
        string name = "- "+Character.Base.Name+" -";
        string level = "- Lv. " + Character.Base.GetLevel()+" -";
        string header = (name.PadRight((Width+name.Length) / 2 - level.Length-2,'─')+level).PadLeft(Width-5,'─');
        return new PanelHeader(header);
    }

    internal Renderable VerticalLine(Color? color = null)
    {
        var rows = Enumerable.Repeat(new Markup("|",color), Height).ToArray();
        return new Rows(rows);
    }

    private protected abstract Renderable CreateContent();
}
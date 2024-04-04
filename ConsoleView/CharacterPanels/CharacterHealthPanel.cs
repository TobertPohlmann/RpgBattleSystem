using RpgBattleSystem.Characters;
using Spectre.Console;
using Spectre.Console.Rendering;
using Status = RpgBattleSystem.Enums.Status;

namespace ConsoleView.CharacterPanels;

public class CharacterPanel
{
    private readonly Color _defenseColor = Color.MediumTurquoise;
    private readonly Color _offenseColor = Color.DeepPink1;
    
    private const int Width = 40;
    private readonly int _barWidth;
    private Character _character;

    public CharacterPanel(Character character)
    {
        _character = character;
        _barWidth = (int)(1.0 * Width);
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
        panel.Header = new PanelHeader(_character.Base.Name);
        panel.Width = Width;
        return panel;
    }

    private Renderable CreateContent()
    {
        List<Renderable> contentList = new();
        var maxHealth = _character.Base.GetStatusValueFor(Status.MaxHealth);

        var healthChart = CreateHealthChart(_character.Health, maxHealth);
        contentList.Add(healthChart);
        contentList.Add(CreateStanceChart(_character.Stance.CurrentValue));
/*        contentList.Add(CreateStanceChart(0));


        for (int i = 0; i < 6; i++)
        {
            contentList.Add(CreateStanceChart(-100+i*40));
        }
*/
        return new Rows(contentList);
    }

    private Renderable CreateHealthChart(double health, int maxHealth)
    {
        double healthFraction = health / maxHealth;
        Color color = healthFraction switch
        {
            > 0.85 => Color.CornflowerBlue,
            > 0.6 => Color.Green,
            > 0.4 => Color.Yellow,
            > 0.2 => Color.Red,
            _ => Color.Red1
        };
        
        var healthBar = new BreakdownChart()
            .Width(_barWidth)
            .AddItem("Health", (int)health, color)
            .AddItem("Empty Health", maxHealth - (int)health,Color.Grey);
        healthBar.ShowTagValues(false);
        healthBar.ShowTags(false);
        var title = new Text(" Health").LeftJustified();
        var healthLabel = new Text((int)health + "/" + maxHealth).RightJustified();
        var titleBar = new Columns(title, healthLabel);
        return new Rows(titleBar,healthBar);
    }
    
    private Renderable CreateStanceChart(int stance)
    {
        int offenseActive = stance >= 0 ? stance : 0;
        int defenseActive = stance >= 0 ? 0 : -stance;
        
        var stanceBar = new BreakdownChart()
            .Width(_barWidth)
            .AddItem("Inactive Defense", 100 - defenseActive, _defenseColor.Blend(Color.Grey27,0.9f))
            .AddItem("Active Defense", defenseActive, _defenseColor)
            .AddItem("Active Offense", offenseActive, _offenseColor)
            .AddItem("Inactive Offense", 100 - offenseActive, _offenseColor.Blend(Color.Grey27,0.9f));

        stanceBar.ShowTagValues(false);
        stanceBar.ShowTags(false);

        var title = new Text(" Stance").LeftJustified();
        var stanceCursor = CreateStanceCursor(stance);
        return new Rows(title,stanceBar,stanceCursor);
    }

    private Renderable CreateStanceCursor(int stance)
    {
        var stanceCursorText = stance switch
        {
            < 0 => ("| " + stance),
            _ => (stance + " |").PadLeft(5,' ')
        };
            
        Color? activeColor = stance switch
        {
            < 0 => _defenseColor,
            > 0 => _offenseColor,
            _ => null
        };
        int paddingOffset = stance >= 0 ? -5 : 0;
        int padding = _barWidth * (100 + stance) / 200 + paddingOffset;
        var stanceCursorStyle = new Style(foreground: activeColor, decoration: Decoration.Bold);
        return new Padder(new Markup(stanceCursorText,stanceCursorStyle)).PadLeft(padding).PadTop(0);
    }

}
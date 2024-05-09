using ConsoleView.CommonElements;
using RpgBattleSystem.Characters;
using Spectre.Console;
using Spectre.Console.Rendering;
using Attribute = RpgBattleSystem.Enums.Attribute;
using Status = RpgBattleSystem.Enums.Status;

namespace ConsoleView.CharacterPanels.StandardPanel;

public class HealthSection : RenderableWrapper
{
    public Rows Renderable { get; private set; } = new();

    private Character _character;
    private int _barWidth;
    private readonly Color _defenseColor = ColorRegistry.For(Attribute.Immunity);
    private readonly Color _offenseColor = Color.DeepPink1;
    
    public HealthSection(Character character, int barWidth)
    {
        _barWidth = barWidth;
        _character = character;
        Render();
    }

    public override void Render()
    {
        var maxHealth = _character.Base.GetStatusValueFor(Status.MaxHealth);
        Renderable = new Rows(
            CreateHealthChart(_character.Health, maxHealth),
            CreateStanceChart(_character.Stance.CurrentValue));        
    }
    
    private Renderable CreateHealthChart(double health, int maxHealth)
    {
        double healthFraction = health / maxHealth;
        Color color = healthFraction switch
        {
            > 0.85 => ColorRegistry.For(Attribute.Vitality),
            > 0.6 => Color.Green,
            > 0.4 => Color.Yellow,
            > 0.2 => Color.Red,
            _ => Color.Red1
        };

        var healthBar = new BreakdownChart()
            .Width(_barWidth)
            .AddItem("Health", (int)health, color)
            .AddItem("Empty Health", maxHealth - (int)health, Color.Grey);
        healthBar.ShowTagValues(false);
        healthBar.ShowTags(false);

        var title = new Markup("Health",ColorRegistry.HeadlineStyle).LeftJustified();
        var healthLabel = new Text((int)health + "/" + maxHealth).RightJustified();
        var titleBar = new Columns(title, healthLabel);
        return new Rows(titleBar, healthBar);
    }

    private Renderable CreateStanceChart(int stance)
    {
        int offenseActive = stance >= 0 ? stance : 0;
        int defenseActive = stance >= 0 ? 0 : -stance;

        var stanceBar = new BreakdownChart()
            .Width(_barWidth)
            .AddItem("Inactive Defense", 100 - defenseActive, _defenseColor.Blend(Color.Grey27, 0.9f))
            .AddItem("Active Defense", defenseActive, _defenseColor)
            .AddItem("Active Offense", offenseActive, _offenseColor)
            .AddItem("Inactive Offense", 100 - offenseActive, _offenseColor.Blend(Color.Grey27, 0.9f));

        stanceBar.ShowTagValues(false);
        stanceBar.ShowTags(false);

        var title = new Markup("Stance",ColorRegistry.HeadlineStyle).LeftJustified();
        var stanceCursor = CreateStanceCursor(stance);
        return new Rows(title, stanceBar, stanceCursor);
    }

    private Renderable CreateStanceCursor(int stance)
    {
        var stanceCursorText = stance switch
        {
            < 0 => ("| " + stance),
            _ => (stance + " |").PadLeft(5, ' ')
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
        return new Padder(new Markup(stanceCursorText, stanceCursorStyle)).PadLeft(padding).PadTop(0).PadBottom(0);
    }
}
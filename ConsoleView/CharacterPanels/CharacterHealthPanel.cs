using RpgBattleSystem.Characters;
using RpgBattleSystem.Equipment;
using Spectre.Console;
using Spectre.Console.Rendering;
using Attribute = RpgBattleSystem.Enums.Attribute;
using Status = RpgBattleSystem.Enums.Status;

namespace ConsoleView.CharacterPanels;

public class CharacterHealthPanel : CharacterSubPanel
{
    private readonly Color _defenseColor = ColorRegistry.For(Attribute.Immunity);
    private readonly Color _offenseColor = Color.DeepPink1;
    private readonly int _barWidth;

    public bool ShowWeaponSection { get; set; } = true;

    public CharacterHealthPanel(Character character) : base(character)
    {
        _barWidth = (int)(0.9 * Width);
    }

    private protected override Renderable CreateContent()
    {
        List<Renderable> contentList = new();
        contentList.Add(CreateHealthSection());
        if (ShowWeaponSection && Character.Equipment.HasWeaponEquipped())
        {
            contentList.Add(VerticalLine());
            contentList.Add(CreateWeaponSection());
        }
        return new Columns(contentList);
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

        var title = new Markup("Health",HeadlineColor).LeftJustified();
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

        var title = new Markup("Stance",HeadlineColor).LeftJustified();
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

    private Renderable CreateHealthSection()
    {
        var maxHealth = Character.Base.GetStatusValueFor(Status.MaxHealth);
        return new Rows(
            CreateHealthChart(Character.Health, maxHealth),
            CreateStanceChart(Character.Stance.CurrentValue));
    }

    private Renderable CreateWeaponSection()
    {
        Grid grid = new Grid();
        grid.AddColumn(); //Waffe
        grid.AddColumn(); //Angriff
        grid.AddColumn(); //Fertigkeit

        var headline = new IRenderable[3];
        
        headline[0] = new Markup("Weapons", HeadlineColor);
        headline[1] = new Markup("");
        headline[2] = new Markup("Skills",HeadlineColor);

        grid.AddRow(headline);
        AddRowsFor(grid,Character.Equipment.Weapon1);
        AddRowsFor(grid,Character.Equipment.Weapon2);
        AddRowsFor(grid,Character.Equipment.Weapon3);
        return grid;
    }

    private void AddRowsFor(Grid grid, Weapon? weapon)
    {
        if (weapon == null)
        {
            return;
        }
        
        var rows = new List<IRenderable[]>();
        int count = 0;
        foreach (var skill in weapon.Skills)
        {
            rows.Add(new IRenderable[3]);
            rows[count][0] = new Text("");
            rows[count][1] = new Text("");
            rows[count][2] = new Markup(skill.Name);
            count++;
        }
        rows[0][0] = new Markup(weapon.Name);
        rows[0][1] = new Markup(weapon.BaseAttack+"");

        foreach (var row in rows)
        {
            grid.AddRow(row);
        }
    }
}
using RpgBattleSystem.Characters;
using Spectre.Console;
using Spectre.Console.Rendering;
using Status = RpgBattleSystem.Enums.Status;
using Attribute = RpgBattleSystem.Enums.Attribute;

namespace ConsoleView.CharacterPanels;

public class CharacterBasePanel : CharacterSubPanel
{
    public CharacterBasePanel(Character character) : base(character) 
    {}
    
    private protected override Renderable CreateContent()
    {
        return new Columns(CreateLeftColumn(),CreateRightColumn());
    }

    private Renderable CreateLeftColumn()
    {
        var grid = new Grid();

        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddRow(CreateStatRow(Attribute.Vitality,Status.MaxHealth));
        grid.AddRow(CreateStatRow(Attribute.Endurance,Status.EquipLoad));
        grid.AddRow(CreateStatRow(Attribute.Strength,Status.StrikeForce));
        grid.AddRow(CreateStatRow(Attribute.Dexterity,Status.Technique));
        grid.AddRow(CreateStatRow(Attribute.Focus,Status.Precision));
        grid.AddRow(CreateStatRow(Attribute.Swiftness,Status.Speed));
        return grid;
    }

    private Renderable CreateRightColumn()
    {
        var grid = new Grid();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddRow(CreateStatRow(Attribute.Immunity,Status.StrikeDefense));
        grid.AddRow(CreateStatRow(null,Status.CutDefense));
        grid.AddRow(CreateStatRow(null,Status.PierceDefense));
        grid.AddRow(CreateStatRow(null,Status.HeatResistance));
        grid.AddRow(CreateStatRow(null,Status.ColdResistance));
        grid.AddRow(CreateStatRow(null,Status.PoisonResistance));
        return grid;
    }

    private IRenderable[] CreateStatRow(Attribute? attribute, Status status)
    {
        var statRow = new IRenderable[4];
        if (attribute != null)
        {
            statRow[0] = new Markup(attribute.ToString(),ColorRegistry.For(attribute.Value));
            statRow[1] = new Text(""+Character.Base.GetLevelFor(attribute.Value));
        }
        else
        {
            statRow[0] = new Text("");
            statRow[1] = new Text("");
        }

        statRow[2] = new Markup(status.ToString(),ColorRegistry.For(status));
        statRow[3] = new Text(""+Character.Base.GetStatusValueFor(status));
        return statRow;
    }
}
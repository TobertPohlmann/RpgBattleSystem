using RpgBattleSystem.Enums;
using Spectre.Console;
using Attribute = RpgBattleSystem.Enums.Attribute;
using Status = RpgBattleSystem.Enums.Status;

namespace ConsoleView;

public class ColorRegistry
{
    public static Color EnemyColor = Color.DarkOrange;
    public static Color HeroColor = Color.CornflowerBlue;
    public static Style SelectStyle = new Style(foreground:Color.DarkGoldenrod,decoration:Decoration.Bold);
    public static Style HeadlineStyle = new Style(foreground:Color.Grey53,decoration:Decoration.Underline);

    
    public static Color For(Attribute attribute)
    {
        switch (attribute)
        {
            case Attribute.Vitality: return Color.CornflowerBlue;
            case Attribute.Endurance: return Color.Green;
            case Attribute.Swiftness: return Color.Gold3;
            case Attribute.Immunity: return Color.SkyBlue3;
            case Attribute.Strength: return Color.Red;
            case Attribute.Focus: return Color.Red1;
            default: return Color.Magenta1;
        }
    }

    public static Color For(Status status)
    {
        switch (status)
        {
            case Status.MaxHealth: return For(Attribute.Vitality);
            case Status.EquipLoad: return For(Attribute.Endurance);
            case Status.StrikeForce: return For(Attribute.Strength);
            case Status.Technique: return For(Attribute.Dexterity);
            case Status.Precision: return For(Attribute.Focus);
            case Status.Speed: return For(Attribute.Swiftness);
            case Status.StrikeDefense: return For(Attribute.Immunity);
            case Status.CutDefense: return For(Attribute.Immunity);
            case Status.PierceDefense: return For(Attribute.Immunity);
            case Status.ColdResistance: return For(Attribute.Immunity).Blend(Color.White,0.3f);
            case Status.HeatResistance: return For(Attribute.Immunity).Blend(Color.White,0.3f);
            case Status.PoisonResistance: return For(Attribute.Immunity).Blend(Color.White,0.3f);
            default: return Color.White;
        }
    }

    public static Color For(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Strike: return For(Attribute.Strength);
            case AttackType.Cut: return For(Attribute.Dexterity);
            default: return For(Attribute.Focus);
        }
    }
    
    
}
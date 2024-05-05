using RpgBattleSystem.Characters;
using Spectre.Console;

namespace ConsoleView.CharacterPanels;

public class CharacterPanel
{
    private Dictionary<SubPanelType, CharacterSubPanel> _panels = new();
 
    public CharacterPanel(Character character)
    {
        _panels[SubPanelType.Health] = new CharacterHealthPanel(character);
        _panels[SubPanelType.Base] = new CharacterBasePanel(character);
    }

    public Panel GetPanelFor(SubPanelType panelType)
    {
        return _panels[panelType].CreatePanel();
    }
    
}
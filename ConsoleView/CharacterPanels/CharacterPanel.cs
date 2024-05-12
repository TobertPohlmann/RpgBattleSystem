using ConsoleView.CharacterPanels.StandardPanel;
using RpgBattleSystem.Characters;
using RpgBattleSystem.Skills;
using Spectre.Console;

namespace ConsoleView.CharacterPanels;

public class CharacterPanel
{
    private Dictionary<SubPanelType, CharacterSubPanel> _panels = new();
 
    public CharacterPanel(Character character)
    {
        _panels[SubPanelType.Standard] = new CharacterStandardPanel(character);
        _panels[SubPanelType.Stat] = new CharacterStatPanel(character);
    }

    public Skill GetSelectedSkill()
    {
        return (_panels[SubPanelType.Standard] as CharacterStandardPanel).GetSelectedSkill();
    }

    public void SetSkillCursorPosition(int cursorPosition)
    {
        (_panels[SubPanelType.Standard] as CharacterStandardPanel).SetSkillCursorPosition(cursorPosition);
        _panels[SubPanelType.Standard].Render();
    }

    public Panel GetSubPanelFor(SubPanelType panelType)
    {
        _panels[panelType].Render();
        return _panels[panelType].Renderable;
    }

    public void SelectSubPanelFor(SubPanelType panelType)
    {
        _panels[panelType].Select(true);
    }
    
    public void DeselectSubPanelFor(SubPanelType panelType)
    {
        _panels[panelType].Select(false);
    }
}
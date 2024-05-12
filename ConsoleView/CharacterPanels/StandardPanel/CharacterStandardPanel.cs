using RpgBattleSystem.Characters;
using RpgBattleSystem.Skills;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ConsoleView.CharacterPanels.StandardPanel;

public class CharacterStandardPanel : CharacterSubPanel
{
    private WeaponSection _weaponSection;
    private HealthSection _healthSection;
    
    public bool ShowWeaponSection { get; set; } = true;

    public CharacterStandardPanel(Character character) : base(character)
    {
        _healthSection = new HealthSection(character, (int)(0.9 * Width));
        _weaponSection = new WeaponSection(character.Equipment);
    }

    public void SetSkillCursorPosition(int cursorPosition)
    {
        _weaponSection.SetCursorPosition(cursorPosition);
    }

    public Skill GetSelectedSkill()
    {
        return _weaponSection.GetSelectedSkill();
    }

    private protected override Renderable CreateContent()
    {
        List<Renderable> contentList = new();

        contentList.Add(_healthSection.Renderable);
        if (ShowWeaponSection && Character.Equipment.HasWeaponEquipped())
        {
            _weaponSection.Render();
            contentList.Add(VerticalLine());
            contentList.Add(_weaponSection.Renderable);
        }
        return new Columns(contentList);
    }

    public override void Select(bool value)
    {
        _selected = value;
        _weaponSection.Select(value);
    }
}
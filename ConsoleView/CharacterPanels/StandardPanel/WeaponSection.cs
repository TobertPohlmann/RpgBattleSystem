using ConsoleView.CommonElements;
using RpgBattleSystem.Characters;
using RpgBattleSystem.Equipment;
using RpgBattleSystem.Skills;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ConsoleView.CharacterPanels.StandardPanel;

internal class WeaponRow
{
    public SelectableMarkup? WeaponName { get; set; }
    public SelectableMarkup? AttackValue { get; set; }
    public SelectableMarkup[] SkillNames { get; } = new SelectableMarkup[2];

    public Skill[] Skills { get; } = new Skill[2];
};

public class WeaponSection : SelectableElement
{
    public Grid Renderable { get; private set; }
    private WeaponRow[] _weaponRows = new WeaponRow[3];
    private readonly CharacterEquipment _equipment;
    private int _cursorPosition = 0;
    
    public WeaponSection(CharacterEquipment equipment)
    {
        _equipment = equipment;
        Render();
    }

    public Skill GetSelectedSkill()
    {
        return _weaponRows[_cursorPosition / 2].Skills[_cursorPosition % 2];
    }

    public void SetCursorPosition(int cursorPosition)
    {
        int maxPosition = _weaponRows.Where(x => x.WeaponName != null).Count()*2-1;
        if (cursorPosition >= 0 && cursorPosition <= maxPosition)
        {
            _cursorPosition = cursorPosition;
            return;
        }
        throw new Exception("Cursor was set to " + cursorPosition + ", but must be between 0 and " + maxPosition);
    }
    
    public override void Render()
    {
        Renderable = new();
        Renderable.AddColumn(); //Waffe
        Renderable.AddColumn(); //Angriff
        Renderable.AddColumn(); //Fertigkeit

        var headline = new IRenderable[3];

        headline[0] = new Markup("Weapons", ColorRegistry.HeadlineStyle);
        headline[1] = new Markup("");
        headline[2] = new Markup("Skills", ColorRegistry.HeadlineStyle);
        
        Renderable.AddRow(headline);
        AddRowsFor(_equipment.Weapon1,0);
        AddRowsFor(_equipment.Weapon2,1);
        AddRowsFor(_equipment.Weapon3,2);
    }

    private void AddRowsFor(Weapon? weapon, int slot)
    {
        if (weapon == null)
        {
            return;
        }

        _weaponRows[slot] = new();
        bool highlightWeapon = _selected && _cursorPosition / 2 == slot;
        _weaponRows[slot].AttackValue = new SelectableMarkup(weapon.BaseAttack + "", highlightWeapon);
        _weaponRows[slot].WeaponName = new SelectableMarkup(weapon.Name, highlightWeapon);
        
        var rows = new List<IRenderable[]>();
        int count = 0;

        foreach (var skill in weapon.Skills)
        {
            bool highlightSkill = highlightWeapon && _cursorPosition == count + 2*slot;
            _weaponRows[slot].SkillNames[count] = new SelectableMarkup(skill.Name, highlightSkill);
            _weaponRows[slot].Skills[count] = skill;
            
            rows.Add(new IRenderable[3]);
            rows[count][0] = new Text("");
            rows[count][1] = new Text("");
            rows[count][2] = _weaponRows[slot].SkillNames[count].Renderable;
            count++;
        }
        
        rows[0][0] = _weaponRows[slot].WeaponName.Renderable;
        rows[0][1] = _weaponRows[slot].AttackValue.Renderable;

        foreach (var row in rows)
        {
            Renderable.AddRow(row);
        }
    }
}
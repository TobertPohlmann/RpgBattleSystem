using ConsoleView.BattleScreen;
using ConsoleView.CharacterPanels;
using RpgBattleSystem.BattleSystem.BattleProceedings;
using RpgBattleSystem.Characters;
using RpgBattleSystem.Enums;
using RpgBattleSystem.Skills;
using Spectre.Console;

namespace GameController;

public class TurnInputRequest
{
    private BattleScreen _screen;
    private LiveDisplayContext _ctx;
    private Character _character;
    private CharacterPanel _panel;
    
    private int _skillCursorPosition = 0;
    private int _maxSkillCursorPosition;
    private ConsoleKey[] _validSkillSelectKeys = new ConsoleKey[]
    {
        ConsoleKey.UpArrow,
        ConsoleKey.DownArrow,
        ConsoleKey.Enter
    };
    private ConsoleKey[] _validTargetSelectKeys = new ConsoleKey[]
    {
        ConsoleKey.UpArrow,
        ConsoleKey.DownArrow,
        ConsoleKey.Enter
    };
    
    public TurnInputRequest(Character character, BattleScreen screen, LiveDisplayContext ctx)
    {
        _ctx = ctx;
        _screen = screen;
        _character = character;
        _panel = _screen.GetPanelFor(_character);
        _maxSkillCursorPosition = character.Equipment.HasWeaponEquipped() ? 2*character.Equipment.GetNumberOfWeapons()-1 : 0;
    }

    public CharacterTurn GetTurnInput()
    {
        _panel.SelectSubPanelFor(SubPanelType.Standard);
        _screen.Render();
        _ctx.Refresh();
        Skill skill = GetSkillInput();
        _panel.SelectSubPanelFor(SubPanelType.Standard);
        List<Character> targets = new List<Character>();
        return new CharacterTurn(_character, skill, targets);
    }

    private List<Character> GetTargetFor(Skill skill)
    {
        switch (skill.Scope)
        {
            case TargetScope.Self: return new List<Character> {_character};
            case TargetScope.OwnParty: return _screen.Battle.HeroParty;
            case TargetScope.OpposingParty: return _screen.Battle.EnemyParty;
            case TargetScope.Ally: return new List<Character> {GetTargetInput(_screen.Battle.HeroParty)};
            default: return new List<Character> {GetTargetInput(_screen.Battle.EnemyParty)};
        }
    }

    private Character GetTargetInput(List<Character> targetPool)
    {
        int maxTargetCursor = targetPool.Count;
        int targetCursor = 0;
        return targetPool[0];
    }

    private Skill GetSkillInput()
    {
        while(true)
        {
            ConsoleKey validInput = WaitForValidInput();
            if (validInput == ConsoleKey.Enter) { break; }
            
            switch (validInput)
            {
                case ConsoleKey.DownArrow: CursorDown();
                    break;
                case ConsoleKey.UpArrow: CursorUp();
                    break;
            }
            _panel.SetSkillCursorPosition(_skillCursorPosition);
            _screen.WriteMessage("Pressed "+validInput);
            _screen.Render();
            _ctx.Refresh();
        }
        return _panel.GetSelectedSkill();
    }
    
    private void CursorUp()
    {
        _skillCursorPosition = (_skillCursorPosition - 1 >= 0) ? _skillCursorPosition - 1 : _maxSkillCursorPosition;
    }

    private void CursorDown()
    {
        _skillCursorPosition = (_skillCursorPosition + 1 <= _maxSkillCursorPosition) ? _skillCursorPosition + 1 : 0;
    }

    private ConsoleKey WaitForValidInput()
    {
        ConsoleKeyInfo userInput;
        do
        {
            userInput = Console.ReadKey();
        } while (!_validSkillSelectKeys.Contains(userInput.Key));
        return userInput.Key;
    }
    
}
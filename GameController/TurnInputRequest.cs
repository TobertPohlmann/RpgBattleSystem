using ConsoleView.BattleScreen;
using ConsoleView.CharacterPanels;
using RpgBattleSystem.BattleSystem.BattleProceedings;
using RpgBattleSystem.Characters;
using RpgBattleSystem.Skills;

namespace GameController;

public class TurnInputRequest
{
    private BattleScreen _screen;
    private Character _character;
    private CharacterPanel _panel;
    
    private int _skillCursorPosition = 0;
    private int _maxSkillCursorPosition;
    private ConsoleKey[] _validKeys = new ConsoleKey[]
    {
        ConsoleKey.UpArrow,
        ConsoleKey.DownArrow,
        ConsoleKey.Enter
    };
    
    public TurnInputRequest(Character character, BattleScreen screen)
    {
        _screen = screen;
        _character = character;
        _panel = _screen.GetPanelFor(_character);
        _maxSkillCursorPosition = character.Equipment.HasWeaponEquipped() ? 2*character.Equipment.GetNumberOfWeapons()-1 : 0;
    }

    public CharacterTurn GetTurnInput()
    {
        Skill skill = GetSkillInput();
        List<Character> targets = new List<Character>();
        return new CharacterTurn(_character, skill, targets);
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
            _screen.Draw();
        }
        return _panel.GetSelectedSkill();
    }
    
    private void CursorUp()
    {
        _skillCursorPosition = (_skillCursorPosition + 1 >= 0) ? _skillCursorPosition - 1 : _maxSkillCursorPosition;
    }

    private void CursorDown()
    {
        _skillCursorPosition = (_skillCursorPosition + 1 >= 0) ? _skillCursorPosition - 1 : _maxSkillCursorPosition;
    }

    private ConsoleKey WaitForValidInput()
    {
        ConsoleKeyInfo userInput;
        do
        {
            userInput = Console.ReadKey();
        } while (_validKeys.Contains(userInput.Key));
        return userInput.Key;
    }
    
}
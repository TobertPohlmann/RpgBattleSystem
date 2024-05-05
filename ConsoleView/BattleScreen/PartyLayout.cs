using ConsoleView.CharacterPanels;
using RpgBattleSystem.Characters;
using Spectre.Console;

namespace ConsoleView.BattleScreen;

public class PartyLayout
{
    private string _name;
    private List<CharacterPanel> _characterPanels = new();
    public Layout Layout;

    public PartyLayout(string name, List<Character> party)
    {
        _name = name;
        foreach (var character in party)
        {
            _characterPanels.Add(new CharacterPanel(character));
        }
        
        Layout = new Layout(_name)
            .SplitColumns(GetPartyLayout(party));
        //Layout.MinimumSize(CharacterSubPanel.Height);
    }
    
    private Layout[] GetPartyLayout(List<Character> party)
    {
        var partyLayout = new Layout[party.Count];
        for (int i = 0; i < party.Count; i++)
        {
            partyLayout[i] = new Layout(""+i);
        }
        return partyLayout;
    }

    public void UpdateLayout(SubPanelType panelType)
    {
        for (int i = 0; i < _characterPanels.Count; i++)
        {
            Layout[_name]["" + i].Update(
                Align.Center(
                    _characterPanels[i].GetPanelFor(panelType)
                    )
                );
        }
    }
    
}
using ConsoleView.CharacterPanels;
using RpgBattleSystem.Characters;
using Spectre.Console;

namespace ConsoleView.BattleScreen;

public class PartyLayout
{
    private string _name;
    public Dictionary<Character,CharacterPanel> CharacterPanels = new();
    public Layout Layout;

    public PartyLayout(string name, List<Character> party)
    {
        _name = name;
        foreach (var character in party)
        {
            CharacterPanels[character] = new CharacterPanel(character);
        }
        
        Layout = new Layout(_name)
            .SplitColumns(CreatePartyLayout(party));
    }
    
    private Layout[] CreatePartyLayout(List<Character> party)
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
        int i = 0;
        foreach (var panel in CharacterPanels.Values)
        {
            Layout[_name]["" + i].Update(
                Align.Center(
                    panel.GetSubPanelFor(panelType)
                )
            );
            i += 1;
        }
    }
}
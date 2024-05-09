using ConsoleView.CommonElements;
using Spectre.Console;

namespace ConsoleView.CharacterPanels;

public class SelectableMarkup : SelectableElement
{
    public Markup Renderable { get; private set; }
    private string _text;
    
    public SelectableMarkup(string text, bool selected = false)
    {
        _selected = selected;
        _text = text;
        Render();
    }

    public override void Render()
    {
        Style? style = _selected ? ColorRegistry.SelectStyle : null;
        Renderable = new Markup(_text, style);
    }
}
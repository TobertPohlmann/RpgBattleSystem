namespace ConsoleView.CommonElements;

public abstract class SelectableElement : RenderableWrapper
{
    internal bool _selected = false;

    public void Select(bool value)
    {
        _selected = value;
    }
}
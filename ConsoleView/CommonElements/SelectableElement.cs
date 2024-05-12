namespace ConsoleView.CommonElements;

public abstract class SelectableElement : RenderableWrapper
{
    internal bool _selected = false;

    public virtual void Select(bool value)
    {
        _selected = value;
    }
}
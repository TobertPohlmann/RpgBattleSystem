using Spectre.Console.Rendering;

namespace ConsoleView.CommonElements;

public abstract class RenderableWrapper
{
    public Renderable Renderable;
    public abstract void Render();
}
using Spectre.Console;

namespace ConsoleView.BattleScreen;

public class MessageBox
{
    private int _height = 6;
    public string Message;
    public Layout Layout;
    
    public MessageBox(string name,string message)
    {
        Message = message;
        Layout = new Layout(name);
        Layout.MinimumSize = _height;
    }

    public void UpdateLayout()
    {
        Layout.Update(CreatePanel());
    }

    private Panel CreatePanel()
    {
        var panel = new Panel(Message);
        panel.Height = _height;
        panel.Expand();
        return panel;
    }
}
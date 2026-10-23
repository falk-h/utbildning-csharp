namespace Övning6.Ui;

public class MenuAction(string? description, Action action)
{
    public void Run() => action();

    public override string ToString() => description ?? "";
}
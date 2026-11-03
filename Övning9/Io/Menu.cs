namespace Övning9.Io;

public class Menu(TextReader input, TextWriter output, params List<MenuAction> actions)
{
    public void Run()
    {
        while (true)
        {
            var optionNumber = 1;
            foreach (var action in actions)
            {
                output.WriteLine($"{optionNumber++}. {action}.");
            }

            output.Write("> ");

            var line = input.ReadLine();

            if (!int.TryParse(line, out var index) || index < 1 || index > actions.Count)
            {
                output.WriteLine($"Enter a number from 1 to {actions.Count}!\n");
                continue;
            }

            actions[index - 1].Run();
            return;
        }
    }
}
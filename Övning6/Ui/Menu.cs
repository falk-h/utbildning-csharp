namespace Övning6.Ui;

public class Menu(params MenuAction[] actions)
{
    public void Run()
    {
        while (true)
        {
            var optionNumber = 1;
            foreach (var action in actions)
            {
                Console.WriteLine($"{optionNumber++}. {action}.");
            }

            Console.Write("> ");

            var input = Console.ReadLine();

            if (!int.TryParse(input, out var index) || index < 1 || index > actions.Length)
            {
                Console.WriteLine($"Enter a number from 1 to {actions.Length}!\n");
                continue;
            }

            actions[index - 1].Run();
            return;
        }
    }
}
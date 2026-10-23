namespace Övning6.Ui;

public class Menu(params List<MenuAction> actions)
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

            if (!int.TryParse(input, out var index) || index < 1 || index > actions.Count)
            {
                Console.WriteLine($"Enter a number from 1 to {actions.Count}!\n");
                continue;
            }

            actions[index - 1].Run();
            return;
        }
    }
}
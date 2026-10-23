namespace Övning6.Ui;

public static class Input
{
    public static string Prompt(string prompt, string errorMessage = "You have to enter something!")
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.WriteLine(errorMessage);
        }
    }

    public static int PromptInt(string prompt, string errorMessage = "Enter an integer!")
    {
        while (true)
        {
            var input = Prompt(prompt, errorMessage);
            if (int.TryParse(input, out var result))
            {
                return result;
            }

            Console.WriteLine(errorMessage);
        }
    }

    public static VehicleNumber PromptVehicleNumber(string prompt)
    {
        while (true)
        {
            var input = Prompt(prompt);
            try
            {
                return new VehicleNumber(input);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public static T PromptOptions<T>(params List<T> options)
        where T : notnull
    {
        var result = default(T);

        var actions = new List<MenuAction>();
        foreach (var option in options)
        {
            actions.Add(new MenuAction(option.ToString(), () => result = option));
        }

        var menu = new Menu(actions);
        menu.Run();

        return result!;
    }
}
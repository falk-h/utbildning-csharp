namespace Övning9.Io;

public class InputReader(TextReader input, TextWriter output)
{
    public string Prompt(string prompt, string errorMessage = "You have to enter something!")
    {
        while (true)
        {
            output.Write($"{prompt}: ");
            var input1 = input.ReadLine();
            if (!string.IsNullOrWhiteSpace(input1))
            {
                return input1.Trim();
            }

            output.WriteLine(errorMessage);
        }
    }

    public int PromptInt(string prompt, string errorMessage = "Enter an integer!")
    {
        while (true)
        {
            var line = Prompt(prompt, errorMessage);
            if (int.TryParse(line, out var result))
            {
                return result;
            }

            output.WriteLine(errorMessage);
        }
    }

    public VehicleNumber PromptVehicleNumber(string prompt)
    {
        while (true)
        {
            var line = Prompt(prompt);
            try
            {
                return new VehicleNumber(line);
            }
            catch (ArgumentException e)
            {
                output.WriteLine(e.Message);
            }
        }
    }

    public T PromptOptions<T>(params List<T> options)
        where T : notnull
    {
        var result = default(T);

        var actions = options
            .Select(option => new MenuAction(option.ToString(), () => result = option))
            .ToList();

        var menu = new Menu(input, output, actions);
        menu.Run();

        return result!;
    }
}
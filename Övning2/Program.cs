namespace Övning2;

static class Program
{
    public static void Main(string[] args)
    {
        var keepGoing = true;
        while (keepGoing)
        {
            var input = PromptUser(
                """
                Huvudmeny
                1. Ungdom eller pensionär
                2. Pris för sällskap
                3. Upprepa 10 gånger
                4. Det tredje ordet
                0. Stäng

                Välj ett alternativ
                """
            );

            switch (input)
            {
                case "1":
                    YouthOrPensioner();
                    break;
                case "2":
                    PriceForGroup();
                    break;
                case "3":
                    RepeatTenTimes();
                    break;
                case "4":
                    TheThirdWord();
                    break;
                case "0":
                    keepGoing = false;
                    break;
                default:
                    PrintError();
                    break;
            }
        }
    }

    private static void YouthOrPensioner()
    {
        if (!int.TryParse(PromptUser("Ange ålder"), out var age))
        {
            PrintError();
            return;
        }

        var (rate, price) = RateAndPriceForAge(age);
        Console.WriteLine($"{rate}: {price}kr\n");
    }

    private static void PriceForGroup()
    {
        var countInput = PromptUser("Ange antal personer i sällskapet");
        if (!int.TryParse(countInput, out var count))
        {
            PrintError();
            return;
        }

        var total = 0;
        for (var i = 0; i < count; i++)
        {
            var ageInput = PromptUser($"Ange ålder för person {i + 1}");
            if (!int.TryParse(ageInput, out var age))
            {
                PrintError();
                i--; // Try again
            }

            total += RateAndPriceForAge(age).Price;
        }

        Console.WriteLine($"Totalpris för {count} personer: {total}kr\n");
    }

    private static (string Rate, int Price) RateAndPriceForAge(int age)
    {
        if (age < 20)
        {
            return ("Ungdomspris", 80);
        }
        else if (age > 64)
        {
            return ("Pensionärspris", 90);
        }
        else
        {
            return ("Standardpris", 120);
        }
    }

    private static void RepeatTenTimes()
    {
        var input = PromptUser("Skriv in en mening");
        for (var i = 0; i < 10; i++)
        {
            Console.WriteLine(input);
        }

        Console.WriteLine();
    }

    private static void TheThirdWord()
    {
        var input = PromptUser("Ange en mening med minst 3 ord");

        var thirdWord = input
            .Split(' ')
            .Where(word => word is not "") // Allow multiple spaces
            .Skip(2)
            .FirstOrDefault();

        if (thirdWord is null)
        {
            PrintError();
            return;
        }

        Console.WriteLine($"Tredje ordet: {thirdWord}\n");
    }

    private static string PromptUser(string prompt)
    {
        Console.Write($"{prompt}: ");

        var input = Console.ReadLine();
        if (input is null)
        {
            // stdin is closed, no reason to keep running
            Environment.Exit(0);
        }

        return input;
    }

    private static void PrintError()
    {
        Console.WriteLine("Inmatningsfel\n");
    }
}
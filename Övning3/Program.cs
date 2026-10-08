namespace ExceptionsDemo;

internal class Program
{
    private const string FileName = "numbers.txt";

    private static int Main()
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, FileName);
            var result = ProcessFile(path);

            Console.WriteLine($"Resultat: {result}");
            return 0;
        }
        catch (IOException)
        {
            // Specifikt fel om filen inte finns
            Console.Error.WriteLine($"Kunde inte läsa filen '{FileName}'");
        }
        catch (FormatException)
        {
            // Specifikt fel om texten inte kan tolkas som tal
            Console.Error.WriteLine("Formatfel");
        }
        catch (DivideByZeroException)
        {
            // Specifikt fel om nolldivision
            Console.Error.WriteLine("Kan inte dividera med noll");
        }
        catch (EmptyFileException)
        {
            Console.Error.WriteLine("Filen är tom");
        }
        catch (Exception)
        {
            // Fallback för alla övriga obekanta fel
            Console.Error.WriteLine("Okänt fel");
        }

        return 1; // Kan bara komma hit om någon exception kastades
    }

    // Exempel på metod som själv kastar ett undantag (throw)
    private static double ProcessFile(string fileName)
    {
        var firstLine = File.ReadLines(fileName).FirstOrDefault() ??
                        throw new EmptyFileException();

        var number = int.Parse(firstLine); // Kan ge FormatException

        if (number == 0)
        {
            throw new DivideByZeroException();
        }

        return 100.0 / number;
    }
}
using System.Text.RegularExpressions;

namespace Övning6;

public record VehicleNumber
{
    public string Number
    {
        get;
        init
        {
            var normalized = NormalizeRegistrationNumber(value);
            field = Regex.IsMatch(normalized, "[A-Z]{3}[0-9]{2}[A-Z0-9]")
                ? normalized
                : throw new ArgumentException($"Registration number '{value}' is invalid");
        }
    }

    public VehicleNumber(string number)
    {
        Number = number;
    }

    private static string NormalizeRegistrationNumber(string registrationNumber) =>
        Regex.Replace(registrationNumber, @"\s", "").ToUpperInvariant();

    public override string ToString() => Number;
}
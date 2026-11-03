using Övning9.Io;
using Övning9.Vehicles;

namespace Övning9;

internal static class Program
{
    private static Garage<Vehicle> _garage = new(40);

    public static void Main()
    {
        Console.CancelKeyPress += (_, _) => Environment.Exit(0); // Exit gracefully on ^C
        var garageHandler = new GarageHandler(40);
        var ui = new Ui(garageHandler, Console.In, Console.Out);
        ui.Run();
    }
}
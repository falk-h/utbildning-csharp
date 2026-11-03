using Övning9.Io;
using Övning9.Vehicles;

namespace Övning9;

internal static class Program
{
    public static void Main()
    {
        Console.CancelKeyPress += (_, _) => Environment.Exit(0); // Exit gracefully on ^C
        
        var garage = new Garage<Vehicle>(40);
        var garageHandler = new GarageHandler(garage);
        var ui = new Ui(garageHandler, Console.In, Console.Out);
        
        ui.Run();
    }
}
using Övning9.Vehicles;

namespace Övning9.Io;

public class Ui(IGarageHandler garageHandler, TextReader input, TextWriter output)
{
    private readonly InputReader _inputReader = new(input, output);

    public void Run()
    {
        FillWithDefaultVehicles();

        var exit = false;
        var menu = new Menu(
            input,
            output,
            new MenuAction("List vehicles", ListVehicles),
            new MenuAction("Count by type", CountByType),
            new MenuAction("Create a new garage", CreateNewGarage),
            new MenuAction("Add a new vehicle", AddNewVehicle),
            new MenuAction("Remove a vehicle", RemoveVehicle),
            new MenuAction("Search for a vehicle", Search),
            new MenuAction("Look up a vehicle by registration number", FindByRegistrationNumber),
            new MenuAction("Fill the garage with default vehicles", FillWithDefaultVehicles),
            new MenuAction("Quit", () => exit = true)
        );

        while (true)
        {
            menu.Run();

            if (exit)
            {
                return;
            }

            output.WriteLine();
        }
    }

    private void ListVehicles()
    {
        if (garageHandler.IsEmpty())
        {
            output.WriteLine("<no vehicles>");
            return;
        }

        foreach (var v in garageHandler.GetAllVehicles())
        {
            output.WriteLine(v);
        }
    }

    private void CountByType()
    {
        if (garageHandler.IsEmpty())
        {
            output.WriteLine("<empty>");
            return;
        }

        var counts = garageHandler.CountByType();

        foreach (var (name, count) in counts)
        {
            output.WriteLine($"{char.ToUpperInvariant(name[0])}{name[1..]}: {count}");
        }
    }

    private void CreateNewGarage()
    {
        var size = _inputReader.PromptPositiveInt("Enter the size of the garage");
        garageHandler.ReplaceGarage(size);
        output.WriteLine("Done!");
    }

    private void AddNewVehicle()
    {
        output.WriteLine("Pick the vehicle's color:");
        var color = _inputReader.PromptOptions([.. Enum.GetValues<Color>()]);
        var registrationNumber =
            _inputReader.PromptVehicleNumber("Enter the vehicle's registration number");

        Vehicle? vehicle = null;
        var menu = new Menu(
            input,
            output,
            new MenuAction("Car", () =>
            {
                var horsepower = _inputReader.PromptPositiveInt("Enter horsepower");
                vehicle = new Car(registrationNumber, color, horsepower);
            }),
            new MenuAction("Motorcycle", () =>
            {
                var lengthCm = _inputReader.PromptPositiveInt("Enter length (cm)");
                vehicle = new Motorcycle(registrationNumber, color, lengthCm);
            }),
            new MenuAction("Airplane", () =>
            {
                var numberOfEngines = _inputReader.PromptPositiveInt("Enter number of engines");
                vehicle = new Airplane(registrationNumber, color, numberOfEngines);
            }),
            new MenuAction("Bus", () =>
            {
                var numberOfSeats = _inputReader.PromptPositiveInt("Enter number of seats");
                vehicle = new Bus(registrationNumber, color, numberOfSeats);
            }),
            new MenuAction("Boat", () =>
            {
                var cylinderVolumeCc =
                    _inputReader.PromptPositiveInt("Enter cylinder volume (cm³)");
                vehicle = new Boat(registrationNumber, color, cylinderVolumeCc);
            })
        );
        menu.Run();

        if (TryAddVehicle(vehicle!))
        {
            output.WriteLine("Done!");
        }
    }

    private bool TryAddVehicle(Vehicle vehicle)
    {
        try
        {
            garageHandler.AddVehicle(vehicle!);
            return true;
        }
        catch (GarageIsFullException e)
        {
            output.WriteLine($"Couldn't add vehicle: {e.Message}");
            return false;
        }
        catch (RegistrationNumberNotUniqueException e)
        {
            output.WriteLine($"Couldn't add vehicle: {e.Message}");
            return false;
        }
    }

    private void RemoveVehicle()
    {
        if (garageHandler.IsEmpty())
        {
            output.WriteLine("The garage is empty!");
            return;
        }

        var vehicles = garageHandler.GetAllVehicles();
        var actions =
            vehicles.Select(vehicle => new MenuAction(vehicle.ToString(),
                    () =>
                    {
                        garageHandler.RemoveVehicle(vehicle);
                        output.WriteLine("Done!");
                    }))
                .ToList();

        actions.Add(new MenuAction("Cancel", () => { }));

        var menu = new Menu(input, output, actions);
        output.WriteLine("Choose a vehicle to remove:");
        menu.Run();
    }

    private void Search()
    {
        var filters = new List<SearchFilter>();
        var menu = new Menu(
            input,
            output,
            new MenuAction("Color", () =>
            {
                output.WriteLine("Choose a color to search for:");
                var color = _inputReader.PromptOptions([.. Enum.GetValues<Color>()]);
                filters.Add(new ColorFilter(color));
            }),
            new MenuAction("Number of wheels", () =>
            {
                var numberOfWheels =
                    _inputReader.PromptPositiveInt("Enter the number of wheels to search for");
                filters.Add(new NumberOfWheelsFilter(numberOfWheels));
            }),
            new MenuAction("Vehicle type", () =>
            {
                output.WriteLine("Choose a vehicle type to search for:");
                var menu = new Menu(
                    input,
                    output,
                    new MenuAction("Motorcycle",
                        () => filters.Add(new TypeFilter(typeof(Motorcycle)))),
                    new MenuAction("Airplane",
                        () => filters.Add(new TypeFilter(typeof(Airplane)))),
                    new MenuAction("Car",
                        () => filters.Add(new TypeFilter(typeof(Car)))),
                    new MenuAction("Bus",
                        () => filters.Add(new TypeFilter(typeof(Bus)))),
                    new MenuAction("Boat",
                        () => filters.Add(new TypeFilter(typeof(Boat))))
                );
                menu.Run();
            })
        );

        var userIsDone = false;
        var doneMenu = new Menu(
            input,
            output,
            new MenuAction("Yes", () => { }),
            new MenuAction("No", () => userIsDone = true)
        );

        while (!userIsDone)
        {
            output.WriteLine("Pick a search criterion:");
            menu.Run();

            output.Write($"Found the following vehicles that {filters[0].Description}");
            for (var i = 1; i < filters.Count; i++)
            {
                output.Write(", ");
                if (i == filters.Count - 1)
                {
                    output.Write("and ");
                }

                output.Write(filters[i].Description);
            }

            output.WriteLine(":");

            var vehicles = garageHandler.FindWithFilters(filters);
            var output1 = string.Join(Environment.NewLine, vehicles);
            output.WriteLine(string.IsNullOrWhiteSpace(output1) ? "<no results>" : output1);

            output.WriteLine("Add another criterion?");
            doneMenu.Run();
        }
    }

    private void FindByRegistrationNumber()
    {
        var number = _inputReader.PromptVehicleNumber("Enter the number to search for");

        output.WriteLine(
            garageHandler.FindByRegistrationNumber(number)?.ToString() ?? "Not found");
    }

    private void FillWithDefaultVehicles()
    {
        List<Vehicle> vehicles =
        [
            new Car(new VehicleNumber("ABC123"), Color.Red, 120),
            new Car(new VehicleNumber("DEF45X"), Color.Blue, 200),
            new Motorcycle(new VehicleNumber("XYZ789"), Color.Black, 220),
            new Airplane(new VehicleNumber("AIR100"), Color.White, 2),
            new Boat(new VehicleNumber("SEA321"), Color.Blue, 1000),
            new Bus(new VehicleNumber("BUS111"), Color.Yellow, 40)
        ];

        var allOk = vehicles.Aggregate(true, (ok, v) => ok & TryAddVehicle(v));
        if (allOk)
        {
            output.WriteLine("Done!");
        }
    }
}
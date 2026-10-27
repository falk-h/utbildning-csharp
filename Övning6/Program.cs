using Övning6.Ui;
using Övning6.Vehicles;

namespace Övning6;

internal static class Program
{
    private static Garage _garage = new(40);

    public static void Main()
    {
        Console.CancelKeyPress += (_, _) => Environment.Exit(0); // Exit gracefully on ^C

        FillWithDefaultVehicles();

        var menu = new Menu(
            new MenuAction("List vehicles", ListVehicles),
            new MenuAction("Count by type", CountByType),
            new MenuAction("Create a new garage", CreateNewGarage),
            new MenuAction("Add a new vehicle", AddNewVehicle),
            new MenuAction("Remove a vehicle", RemoveVehicle),
            new MenuAction("Search for a vehicle", Search),
            new MenuAction("Look up a vehicle by registration number", FindByRegistrationNumber),
            new MenuAction("Fill the garage with default vehicles", FillWithDefaultVehicles),
            new MenuAction("Quit", () => Environment.Exit(0))
        );

        while (true)
        {
            menu.Run();
            Console.WriteLine();
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private static void ListVehicles()
    {
        ShowListOfVehicles(_garage.Vehicles);
    }

    private static void CountByType()
    {
        if (_garage.Vehicles.Count == 0)
        {
            Console.WriteLine("<empty>");
            return;
        }

        var counts = new SortedDictionary<string, int>();
        foreach (var v in _garage.Vehicles)
        {
            var count = counts.GetValueOrDefault(v.Name);
            counts[v.Name] = count + 1;
        }

        foreach (var (name, count) in counts)
        {
            Console.WriteLine($"{char.ToUpperInvariant(name[0])}{name[1..]}: {count}");
        }
    }

    private static void CreateNewGarage()
    {
        var size = Input.PromptInt("Enter the size of the garage");
        _garage = new Garage(size);
        Console.WriteLine("Done!");
    }

    private static void AddNewVehicle()
    {
        Console.WriteLine("Pick the vehicle's color:");
        var color = Input.PromptOptions([.. Enum.GetValues<Color>()]);
        var registrationNumber =
            Input.PromptVehicleNumber("Enter the vehicle's registration number");

        Vehicle? vehicle = null;
        var menu = new Menu(
            new MenuAction("Car", () =>
            {
                var horsepower = Input.PromptInt("Enter horsepower");
                vehicle = new Car(registrationNumber, color, horsepower);
            }),
            new MenuAction("Motorcycle", () =>
            {
                var lengthCm = Input.PromptInt("Enter length (cm)");
                vehicle = new Motorcycle(registrationNumber, color, lengthCm);
            }),
            new MenuAction("Airplane", () =>
            {
                var numberOfEngines = Input.PromptInt("Enter number of engines");
                vehicle = new Airplane(registrationNumber, color, numberOfEngines);
            }),
            new MenuAction("Bus", () =>
            {
                var numberOfSeats = Input.PromptInt("Enter number of seats");
                vehicle = new Bus(registrationNumber, color, numberOfSeats);
            }),
            new MenuAction("Boat", () =>
            {
                var cylinderVolumeCc = Input.PromptInt("Enter cylinder volume (cm³)");
                vehicle = new Boat(registrationNumber, color, cylinderVolumeCc);
            })
        );
        menu.Run();

        try
        {
            _garage.AddVehicle(vehicle!);
            Console.WriteLine("Done!");
        }
        catch (GarageIsFullException e)
        {
            Console.WriteLine($"Couldn't add the vehicle: {e.Message}");
        }
        catch (RegistrationNumberNotUniqueException e)
        {
            Console.WriteLine($"Couldn't add the vehicle: {e.Message}");
        }
    }

    private static void RemoveVehicle()
    {
        if (_garage.Vehicles.Count == 0)
        {
            Console.WriteLine("The garage is empty!");
        }

        var actions = new List<MenuAction>();
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var vehicle in _garage.Vehicles)
        {
            actions.Add(new MenuAction(vehicle.ToString(), () =>
            {
                _garage.RemoveVehicle(vehicle);
                Console.WriteLine("Done!");
            }));
        }

        actions.Add(new MenuAction("Cancel", () => { }));

        var menu = new Menu(actions);
        Console.WriteLine("Choose a vehicle to remove:");
        menu.Run();
    }

    private static void Search()
    {
        var filters = new List<SearchFilter>();
        var menu = new Menu(new MenuAction("Color", () =>
            {
                Console.WriteLine("Choose a color to search for:");
                var color = Input.PromptOptions([.. Enum.GetValues<Color>()]);
                filters.Add(new ColorFilter(color));
            }),
            new MenuAction("Number of wheels", () =>
            {
                var numberOfWheels =
                    Input.PromptInt("Enter the number of wheels to search for");
                filters.Add(new NumberOfWheelsFilter(numberOfWheels));
            }),
            new MenuAction("Vehicle type", () =>
            {
                Console.WriteLine("Choose a vehicle type to search for:");
                var menu = new Menu(
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
        while (!userIsDone)
        {
            Console.WriteLine("Pick a search criterion:");
            menu.Run();

            Console.Write($"Found the following vehicles that {filters[0].Description}");
            for (var i = 1; i < filters.Count; i++)
            {
                Console.Write(", ");
                if (i == filters.Count - 1)
                {
                    Console.Write("and ");
                }

                Console.Write(filters[i].Description);
            }

            Console.WriteLine(":");

            var foundAny = false;
            foreach (var vehicle in _garage.Vehicles)
            {
                if (filters.TrueForAll(f => f.Matches(vehicle)))
                {
                    foundAny = true;
                    Console.WriteLine(vehicle);
                }
            }

            if (!foundAny)
            {
                Console.WriteLine("<no results>");
            }

            Console.WriteLine("Add another criterion?");
            var doneMenu = new Menu(
                new MenuAction("Yes", () => { }),
                new MenuAction("No", () => userIsDone = true)
            );
            doneMenu.Run();
        }
    }

    private static void ShowListOfVehicles(IReadOnlyCollection<Vehicle> vehicles)
    {
        if (vehicles.Count == 0)
        {
            Console.WriteLine("<no vehicles>");
            return;
        }

        foreach (var v in vehicles)
        {
            Console.WriteLine(v);
        }
    }

    private static void FindByRegistrationNumber()
    {
        var number = Input.PromptVehicleNumber("Enter the number to search for");

        var found = false;
        foreach (var vehicle in _garage.Vehicles)
        {
            if (vehicle.RegistrationNumber == number)
            {
                Console.WriteLine(vehicle);
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Not found");
        }
    }

    private static void FillWithDefaultVehicles()
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

        foreach (var vehicle in vehicles)
        {
            try
            {
                _garage.AddVehicle(vehicle);
            }
            catch (GarageIsFullException e)
            {
                Console.WriteLine($"Couldn't add all default vehicles: {e.Message}");
                return;
            }
            catch (RegistrationNumberNotUniqueException e)
            {
                Console.WriteLine($"Couldn't add default vehicle: {e.Message}");
            }
        }
    }
}
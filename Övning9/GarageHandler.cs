using Övning9.Vehicles;

namespace Övning9;

public class GarageHandler(IGarage<Vehicle> garage) : IGarageHandler
{
    public void ReplaceGarage(int newGarageSize)
    {
        garage.Reset(newGarageSize);
    }

    public bool IsEmpty() => !garage.Any();

    public IEnumerable<KeyValuePair<string, int>> CountByType() =>
        garage.CountBy(v => v.Name);

    // TODO: How to handle exceptions / error messages here?
    public void AddVehicle(Vehicle vehicle)
    {
        garage.AddVehicle(vehicle);
    }

    public void RemoveVehicle(Vehicle vehicle)
    {
        garage.RemoveVehicle(vehicle);
    }

    public IEnumerable<Vehicle> GetAllVehicles() => garage;

    public IEnumerable<Vehicle> FindWithFilters(IReadOnlyCollection<SearchFilter> filters) =>
        garage.Where(v => filters.All(f => f.Matches(v)));

    public Vehicle? FindByRegistrationNumber(VehicleNumber registrationNumber) =>
        garage.FirstOrDefault(v => v.RegistrationNumber == registrationNumber);

    public void FillWithDefaultVehicles()
    {
        // TODO
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
                garage.AddVehicle(vehicle);
            }
            catch (GarageIsFullException)
            {
                // TODO
                // _output.WriteLine($"Couldn't add all default vehicles: {e.Message}");
                return;
            }
            catch (RegistrationNumberNotUniqueException)
            {
                // TODO
                // _output.WriteLine($"Couldn't add default vehicle: {e.Message}");
            }
        }
    }
}
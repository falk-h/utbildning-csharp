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

    public void SaveToDisk(string file)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(file);
        File.WriteAllText(file, garage.Serialize());
        Console.WriteLine(File.ReadAllText(file));
    }

    public void LoadFromDisk(string file)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(file);
        garage.Deserialize(File.ReadAllText(file));
    }
}
using Övning9.Vehicles;

namespace Övning9;

public interface IGarageHandler
{
    void ReplaceGarage(int newGarageSize);
    bool IsEmpty();
    IEnumerable<KeyValuePair<string, int>> CountByType();
    void AddVehicle(Vehicle vehicle);
    void RemoveVehicle(Vehicle vehicle);
    IEnumerable<Vehicle> GetAllVehicles();
    IEnumerable<Vehicle> FindWithFilters(IReadOnlyCollection<SearchFilter> filters);
    Vehicle? FindByRegistrationNumber(VehicleNumber registrationNumber);
    public void SaveToDisk(string file);
    public void LoadFromDisk(string file);
}
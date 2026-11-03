using Övning9.Vehicles;

namespace Övning9;

public interface IGarage<T> : IEnumerable<T> where T : Vehicle
{
    void AddVehicle(T vehicle);
    void RemoveVehicle(T vehicle);
    void Reset(int size);
    string Serialize();
    void Deserialize(string json);
}
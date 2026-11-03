using System.Collections;
using System.Text.Json;
using Övning9.Vehicles;

namespace Övning9;

public class Garage<T> : IGarage<T> where T : Vehicle
{
    private List<T> _vehicles = [];
    private int _spaceLeft;

    public Garage(int size)
    {
        Reset(size);
    }

    public void Reset(int size)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);

        _vehicles.Clear();
        _spaceLeft = size;
    }

    public string Serialize() =>
        JsonSerializer.Serialize(new SerializedGarage(_spaceLeft, _vehicles));

    public void Deserialize(string json)
    {
        var deserialized = JsonSerializer.Deserialize<SerializedGarage>(json);
        if (deserialized is null)
        {
            throw new IOException("Failed to deserialize JSON");
        }

        _vehicles = deserialized.Vehicles;
        _spaceLeft = deserialized.SpaceLeft;
    }

    public void AddVehicle(T vehicle)
    {
        if (_spaceLeft < vehicle.Size)
        {
            throw new GarageIsFullException();
        }

        if (_vehicles.Exists(v => v.RegistrationNumber == vehicle.RegistrationNumber))
        {
            throw new RegistrationNumberNotUniqueException(vehicle.RegistrationNumber);
        }

        _spaceLeft -= vehicle.Size;
        _vehicles.Add(vehicle);
    }

    public void RemoveVehicle(T vehicle)
    {
        if (!_vehicles.Contains(vehicle)) return;

        _spaceLeft += vehicle.Size;
        _vehicles.Remove(vehicle);
    }

    private record SerializedGarage(int SpaceLeft, List<T> Vehicles);

    public IEnumerator<T> GetEnumerator() => _vehicles.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class GarageIsFullException() : Exception("Garage is full!");

public class RegistrationNumberNotUniqueException(VehicleNumber registrationNumber)
    : Exception($"Registration number '{registrationNumber}' already exists!");
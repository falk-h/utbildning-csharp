using Övning6.Vehicles;

namespace Övning6;

public class Garage
{
    private readonly List<Vehicle> _vehicles = [];
    private int _spaceLeft;

    public IReadOnlyList<Vehicle> Vehicles => _vehicles;

    public Garage(int size)
    {
        _spaceLeft = size;
    }

    public void AddVehicle(Vehicle vehicle)
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

    public void RemoveVehicle(Vehicle vehicle)
    {
        _vehicles.RemoveAll(v => v.RegistrationNumber == vehicle.RegistrationNumber);
    }
}

public class GarageIsFullException() : Exception("Garage is full!");

public class RegistrationNumberNotUniqueException(VehicleNumber registrationNumber)
    : Exception($"Registration number '{registrationNumber}' already exists!");
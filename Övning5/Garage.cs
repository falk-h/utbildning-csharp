using Övning5.Vehicles;

namespace Övning5;

public class Garage
{
    private Vehicle[] _vehicles;
    private int _spaceLeft;

    public Vehicle[] Vehicles => (Vehicle[])_vehicles.Clone();

    public Garage(int size)
    {
        _vehicles = [];
        _spaceLeft = size;
    }

    public void AddVehicle(Vehicle vehicle)
    {
        if (_spaceLeft < vehicle.Size)
        {
            throw new GarageIsFullException();
        }

        if (!Array.TrueForAll(_vehicles, v => v.RegistrationNumber != vehicle.RegistrationNumber))
        {
            throw new RegistrationNumberNotUniqueException(vehicle.RegistrationNumber);
        }

        _spaceLeft -= vehicle.Size;
        _vehicles = [.. _vehicles, vehicle];
    }

    public void RemoveVehicle(Vehicle vehicle)
    {
        var i = Array.FindIndex(_vehicles, v => v.RegistrationNumber == vehicle.RegistrationNumber);
        _vehicles = [.. _vehicles[..i], .. _vehicles[(i + 1)..]];
    }
}

public class GarageIsFullException() : Exception("Garage is full!");

public class RegistrationNumberNotUniqueException(VehicleNumber registrationNumber)
    : Exception($"Registration number '{registrationNumber}' already exists!");
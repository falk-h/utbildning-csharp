using Övning6.Vehicles;

namespace Övning6;

public abstract class SearchFilter
{
    public abstract bool Matches(Vehicle vehicle);
    public abstract string Description { get; }
}

public class NumberOfWheelsFilter(int number) : SearchFilter
{
    public override bool Matches(Vehicle vehicle) => vehicle.NumberOfWheels == number;
    public override string Description => $"have {number} wheel" + (number == 1 ? "" : "s");
}

public class ColorFilter(Color color) : SearchFilter
{
    public override bool Matches(Vehicle vehicle) => vehicle.Color == color;
    public override string Description => $"are {color.ToString().ToLowerInvariant()}";
}

public class RegistrationNumberFilter(VehicleNumber number) : SearchFilter
{
    public override bool Matches(Vehicle vehicle) => vehicle.RegistrationNumber == number;
    public override string Description => $"have the registration number {number}";
}

public class TypeFilter(Type type) : SearchFilter
{
    public override bool Matches(Vehicle vehicle) => vehicle.GetType() == type;
    public override string Description => $"are of type {type.Name.ToLowerInvariant()}";
}
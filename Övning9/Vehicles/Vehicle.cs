using System.Text.Json.Serialization;

namespace Övning9.Vehicles;

[JsonDerivedType(typeof(Car), nameof(Car))]
[JsonDerivedType(typeof(Airplane), nameof(Airplane))]
[JsonDerivedType(typeof(Motorcycle), nameof(Motorcycle))]
[JsonDerivedType(typeof(Bus), nameof(Bus))]
[JsonDerivedType(typeof(Boat), nameof(Boat))]
public abstract class Vehicle(VehicleNumber registrationNumber, Color color)
{
    public VehicleNumber RegistrationNumber { get; } = registrationNumber;
    public Color Color { get; } = color;
    [JsonIgnore] public abstract int NumberOfWheels { get; }
    [JsonIgnore] public abstract int Size { get; }
    [JsonIgnore] public abstract string Name { get; }
    [JsonIgnore] public abstract string NamePlural { get; }
    protected abstract string ExtraDescription { get; }

    public override string ToString() =>
        $"{RegistrationNumber}: {Color} {Name} with {NumberOfWheels} wheels ({ExtraDescription})";
}
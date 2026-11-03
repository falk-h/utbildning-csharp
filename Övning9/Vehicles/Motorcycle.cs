using System.Text.Json.Serialization;

namespace Övning9.Vehicles;

public class Motorcycle(VehicleNumber registrationNumber, Color color, int lengthCm)
    : Vehicle(registrationNumber, color)
{
    [JsonIgnore] public override int NumberOfWheels => 2;
    [JsonIgnore] public override int Size => 1;
    [JsonIgnore] public override string Name => "motorcycle";
    [JsonIgnore] public override string NamePlural => "motorcycles";
    protected override string ExtraDescription => $"{LengthCm}cm long";
    public int LengthCm { get; } = lengthCm;
}
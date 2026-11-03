using System.Text.Json.Serialization;

namespace Övning9.Vehicles;

public class Car(VehicleNumber registrationNumber, Color color, int horsepower)
    : Vehicle(registrationNumber, color)
{
    [JsonIgnore] public override int NumberOfWheels => 4;
    [JsonIgnore] public override int Size => 2;
    [JsonIgnore] public override string Name => "car";
    [JsonIgnore] public override string NamePlural => "cars";
    protected override string ExtraDescription => $"{Horsepower}hp";
    public int Horsepower { get; } = horsepower;
}
using System.Text.Json.Serialization;

namespace Övning9.Vehicles;

public class Airplane(VehicleNumber registrationNumber, Color color, int numberOfEngines) : Vehicle(registrationNumber, color)
{
    [JsonIgnore] public override int NumberOfWheels => 3;
    [JsonIgnore] public override int Size => 8;
    [JsonIgnore] public override string Name => "airplane";
    [JsonIgnore] public override string NamePlural => "airplanes";
    protected override string ExtraDescription => $"with {NumberOfEngines} engines";
    public int NumberOfEngines { get; } = numberOfEngines;
}
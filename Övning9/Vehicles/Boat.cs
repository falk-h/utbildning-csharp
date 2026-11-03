using System.Text.Json.Serialization;

namespace Övning9.Vehicles;

public class Boat(VehicleNumber registrationNumber, Color color, int cylinderVolumeCc)
    : Vehicle(registrationNumber, color)
{
    [JsonIgnore] public override int NumberOfWheels => 0;
    [JsonIgnore] public override int Size => 4;
    [JsonIgnore] public override string Name => "boat";
    [JsonIgnore] public override string NamePlural => "boats";
    protected override string ExtraDescription => $"{CylinderVolumeCc}cc";
    public int CylinderVolumeCc { get; } = cylinderVolumeCc;
}
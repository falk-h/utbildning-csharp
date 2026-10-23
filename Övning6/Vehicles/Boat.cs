namespace Övning6.Vehicles;

public class Boat(VehicleNumber registrationNumber, Color color, int cylinderVolumeCc)
    : Vehicle(registrationNumber, color)
{
    public override int NumberOfWheels => 0;
    public override int Size => 4;
    public override string Name => "boat";
    public override string NamePlural => "boats";
    protected override string ExtraDescription => $"{CylinderVolumeCc}cc";
    public int CylinderVolumeCc { get; } = cylinderVolumeCc;
}
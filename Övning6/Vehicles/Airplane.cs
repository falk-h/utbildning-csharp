namespace Övning6.Vehicles;

public class Airplane(VehicleNumber registrationNumber, Color color, int numberOfEngines) : Vehicle(registrationNumber, color)
{
    public override int NumberOfWheels => 3;
    public override int Size => 8;
    public override string Name => "airplane";
    public override string NamePlural => "airplanes";
    protected override string ExtraDescription => $"with {NumberOfEngines} engines";
    public int NumberOfEngines { get; } = numberOfEngines;
}
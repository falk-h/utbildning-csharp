namespace Övning5.Vehicles;

public class Motorcycle(VehicleNumber registrationNumber, Color color, int lengthCm)
    : Vehicle(registrationNumber, color)
{
    public override int NumberOfWheels => 2;
    public override int Size => 1;
    public override string Name => "motorcycle";
    public override string NamePlural => "motorcycles";
    protected override string ExtraDescription => $"{LengthCm}cm long";
    public int LengthCm { get; } = lengthCm;
}
namespace Övning6.Vehicles;

public class Car(VehicleNumber registrationNumber, Color color, int horsepower)
    : Vehicle(registrationNumber, color)
{
    public override int NumberOfWheels => 4;
    public override int Size => 2;
    public override string Name => "car";
    public override string NamePlural => "cars";
    protected override string ExtraDescription => $"{Horsepower}hp";
    public int Horsepower { get; } = horsepower;
}
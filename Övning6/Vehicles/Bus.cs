namespace Övning6.Vehicles;

public class Bus(VehicleNumber registrationNumber, Color color, int numberOfSeats) : Vehicle(registrationNumber, color)
{
    public override int NumberOfWheels => 6;
    public override int Size => 4;
    public override string Name => "bus";
    public override string NamePlural => "buses";
    protected override string ExtraDescription => $"{NumberOfSeats} seats";
    public int NumberOfSeats { get; } = numberOfSeats;
}
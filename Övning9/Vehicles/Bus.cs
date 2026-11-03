using System.Text.Json.Serialization;

namespace Övning9.Vehicles;

public class Bus(VehicleNumber registrationNumber, Color color, int numberOfSeats) : Vehicle(registrationNumber, color)
{
    [JsonIgnore] public override int NumberOfWheels => 6;
    [JsonIgnore] public override int Size => 4;
    [JsonIgnore] public override string Name => "bus";
    [JsonIgnore] public override string NamePlural => "buses";
    protected override string ExtraDescription => $"{NumberOfSeats} seats";
    public int NumberOfSeats { get; } = numberOfSeats;
}
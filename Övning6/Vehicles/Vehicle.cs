namespace Övning6.Vehicles;

public abstract class Vehicle(VehicleNumber registrationNumber, Color color)
{
    public VehicleNumber RegistrationNumber { get; } = registrationNumber;
    public Color Color { get; } = color;
    public abstract int NumberOfWheels { get; }
    public abstract int Size { get; }
    public abstract string Name { get; }
    public abstract string NamePlural { get; }
    protected abstract string ExtraDescription { get; }

    public override string ToString() =>
        $"{RegistrationNumber}: {Color} {Name} with {NumberOfWheels} wheels ({ExtraDescription})";
}
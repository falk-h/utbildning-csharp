using JetBrains.Annotations;
using Övning9.Vehicles;

namespace Övning9.Tests;

[TestSubject(typeof(Garage<>))]
public class GarageTest
{
    [Fact]
    public void AddVehicle_TwiceWithSameVehicle_ThrowsException()
    {
        var car = new Car(new VehicleNumber("abc123"), Color.Black, 100);
        var garage = new Garage<Car>(1000);
        garage.AddVehicle(car);

        Assert.Throws<RegistrationNumberNotUniqueException>(() => garage.AddVehicle(car));
    }

    [Fact]
    public void AddVehicle_WhenGarageIsFull_ThrowsException()
    {
        var car1 = new Car(new VehicleNumber("abc123"), Color.Black, 100);
        var car2 = new Car(new VehicleNumber("xyz456"), Color.Green, 200);
        var garage = new Garage<Car>(car1.Size);
        garage.AddVehicle(car1);

        Assert.Throws<GarageIsFullException>(() => garage.AddVehicle(car2));
    }

    [Fact]
    public void AddVehicle_WhenGarageDoesNotHaveEnoughRoom_ThrowsException()
    {
        var plane = new Airplane(new VehicleNumber("abc123"), Color.White, 4);
        var garage = new Garage<Airplane>(plane.Size - 1);

        Assert.Throws<GarageIsFullException>(() => garage.AddVehicle(plane));
    }

    [Fact]
    public void IEnumerable_AfterAddingVehicle_ReturnsVehicle()
    {
        var car = new Car(new VehicleNumber("abc123"), Color.Black, 100);
        var garage = new Garage<Car>(car.Size);

        garage.AddVehicle(car);

        Assert.Single(garage);
        Assert.Contains(car, garage);
    }

    [Fact]
    public void RemoveVehicle_AfterAddingVehicle_RemovesTheVehicle()
    {
        var car = new Car(new VehicleNumber("abc123"), Color.Black, 100);
        var garage = new Garage<Car>(car.Size);
        garage.AddVehicle(car);

        garage.RemoveVehicle(car);

        Assert.Empty(garage);
    }

    [Fact]
    public void RemoveVehicle_AfterAddingMultipleVehicles_RemovesOnlyOneVehicle()
    {
        var car1 = new Car(new VehicleNumber("abc123"), Color.Black, 100);
        var car2 = new Car(new VehicleNumber("def456"), Color.Red, 200);
        var car3 = new Car(new VehicleNumber("ghi789"), Color.Green, 300);
        var garage = new Garage<Car>(car1.Size * 3);
        garage.AddVehicle(car1);
        garage.AddVehicle(car2);
        garage.AddVehicle(car3);

        garage.RemoveVehicle(car2);

        Assert.Contains(car1, garage);
        Assert.Contains(car3, garage);
        Assert.DoesNotContain(car2, garage);
        Assert.Equal(2, garage.Count());
    }

    [Fact]
    public void AddVehicle_AfterRemovingVehicle_Succeeds()
    {
        var car = new Car(new VehicleNumber("abc123"), Color.Black, 100);
        var garage = new Garage<Car>(car.Size);
        garage.AddVehicle(car);

        garage.RemoveVehicle(car);
        garage.AddVehicle(car);

        Assert.Single(garage);
        Assert.Contains(car, garage);
    }
}
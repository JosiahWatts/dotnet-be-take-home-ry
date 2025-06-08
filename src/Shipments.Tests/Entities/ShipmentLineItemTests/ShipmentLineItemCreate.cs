
using RygenTakeHome.API.Entities;

namespace Shipments.Domain.Tests.Entities.ShipmentLineItemTests;

public class ShipmentLineItemCreate
{
    [Fact]
    public void PickupStopMustBeBeforeDropoff()
    {
        Action act = () => new ShipmentLineItem(1, "test", 1, 23.0, 2, 1);
        
        ArgumentException exception = Assert.Throws<ArgumentException>(act);

        Assert.Equal("pickup stop sequence cannot occur after dropoff", exception.Message);
    }

    [Fact]
    public void PickupStopAndDropoffStopCannotBeEqual()
    {
        Action act = () => new ShipmentLineItem(1, "test", 1, 23.0, 1, 1);

        ArgumentException exception = Assert.Throws<ArgumentException>(act);

        Assert.Equal("pickup stop and dropoff stop cannot be equal", exception.Message);
    }

    [Fact]
    public void CannotHaveNegativeOrZeroWeight()
    {
        Action actNegative = () => new ShipmentLineItem(1, "test", 1, -23.0, 1, 2);
        Action actZero = () => new ShipmentLineItem(1, "test", 1, 0.0, 1, 2);

        ArgumentException negativeException = Assert.Throws<ArgumentException>(actNegative);
        ArgumentException zeroException = Assert.Throws<ArgumentException>(actZero);

        Assert.Contains("Required input weight cannot be zero or negative", negativeException.Message);
        Assert.Contains("Required input weight cannot be zero or negative", zeroException.Message);
    }

    [Fact]
    public void CannotHaveZeroOrLessQty()
    {
        Action actQtyNegative = () => new ShipmentLineItem(1, "test", -1, 23.0, 1, 2);
        Action actQtyZero = () => new ShipmentLineItem(1, "test", 0, 23.0, 1, 2);

        ArgumentException negativeException = Assert.Throws<ArgumentException>(actQtyNegative);
        ArgumentException zeroException = Assert.Throws<ArgumentException>(actQtyZero);

        Assert.Contains("Required input qty cannot be zero or negative", negativeException.Message);
        Assert.Contains("Required input qty cannot be zero or negative", zeroException.Message);
    }
}

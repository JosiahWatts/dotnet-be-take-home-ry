using RygenTakeHome.API.Entities;

namespace Shipments.Domain.Tests.Entities.ShipmentStopTests;

public class ShipmentStopCreate
{
    [Fact]
    public void FirstShipmentStopSequenceMustBeZeroMiles()
    {
        Action act = () => new ShipmentStop(1, "123 Main", 1.0);

        ArgumentException negativeException = Assert.Throws<ArgumentException>(act);

        Assert.NotNull(negativeException);
    }

    [Fact]
    public void RemainingStopMilesCannotBeZeroOrNegative()
    {
        Action actNegative = () => new ShipmentStop(2, "123 Main", -1.0);
        Action actZero = () => new ShipmentStop(2, "123 Main", 0.0);

        ArgumentException negativeException = Assert.Throws<ArgumentException>(actNegative);
        ArgumentException zeroException = Assert.Throws<ArgumentException>(actZero);

        Assert.Contains("Required input milesFromPreviousStop", negativeException.Message);
        Assert.Contains("Required input milesFromPreviousStop", zeroException.Message);
    }

    [Fact]
    public void CannotHaveEmptyAddress()
    {
        Action actEmpty = () => new ShipmentStop(1, "", -1.0);

        ArgumentException exception = Assert.Throws<ArgumentException>(actEmpty);

        Assert.NotNull(exception);
    }

    [Fact]
    public void SequenceNumberCannotBeNegativeOrZero()
    {
        Action actNegative = () => new ShipmentStop(0, "123 Main", 1.0);
        Action actZero = () => new ShipmentStop(-1, "123 Main", 2.0);

        ArgumentException negativeException = Assert.Throws<ArgumentException>(actNegative);
        ArgumentException zeroException = Assert.Throws<ArgumentException>(actZero);

        Assert.Contains("Required input sequenceNumber cannot be zero or negative", negativeException.Message);
        Assert.Contains("Required input sequenceNumber cannot be zero or negative", zeroException.Message);

    }
}

using RygenTakeHome.API.Entities;

namespace Shipments.Domain.Tests.Entities.ShipmentTests;

public class ShipmentCreate
{
    [Fact]
    public void CanCreateShipment()
    {
        var shipment = new Shipment(1);

        Assert.Empty(shipment.ShipmentLineItems);
        Assert.Empty(shipment.ShipmentStops);
        Assert.True(shipment.ShipmentId >= 0, "Expected ShipmentId to not be negative");
    }
}
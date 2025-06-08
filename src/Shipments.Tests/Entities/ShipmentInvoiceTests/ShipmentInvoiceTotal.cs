using RygenTakeHome.API.Entities;
using Shipments.Domain.Services;

namespace Shipments.Domain.Tests.Entities.ShipmentInvoiceTests;

public class ShipmentInvoiceTotal
{
    private readonly decimal _testTotal = 1234.34m;

    [Fact]
    public void IsCorrectTotalGivenMileageAllocator()
    {
        var shipment = new Shipment(1);

        shipment.AddStop(new ShipmentStop(1, "The White House", 0.0));
        shipment.AddStop(new ShipmentStop(2, "Bob's House", 425.0));
        shipment.AddStop(new ShipmentStop(3, "World of Coca-Cola", 123.0));

        shipment.AddLineItem(new ShipmentLineItem(1, "tigers", 10, 100.0, 1, 3));
        shipment.AddLineItem(new ShipmentLineItem(1, "lions", 10, 125.0, 1, 2));
        shipment.AddLineItem(new ShipmentLineItem(2, "bears", 5, 150, 2, 3));

        var invoice = ShipmentInvoice.Create(shipment, new MileageCostAllocator());

        Assert.Equal(_testTotal, invoice.TotalCost);
    }

    [Fact]
    public void IsCorrectTotalGivenWeightAllocator()
    {
        var shipment = new Shipment(1);

        shipment.AddStop(new ShipmentStop(1, "The White House", 0.0));
        shipment.AddStop(new ShipmentStop(2, "Bob's House", 425.0));
        shipment.AddStop(new ShipmentStop(3, "World of Coca-Cola", 123.0));

        shipment.AddLineItem(new ShipmentLineItem(1, "tigers", 10, 100.0, 1, 3));
        shipment.AddLineItem(new ShipmentLineItem(1, "lions", 10, 125.0, 1, 2));
        shipment.AddLineItem(new ShipmentLineItem(2, "bears", 5, 150, 2, 3));

        var invoice = ShipmentInvoice.Create(shipment, new WeightCostAllocator());

        Assert.Equal(_testTotal, invoice.TotalCost);
    }
}

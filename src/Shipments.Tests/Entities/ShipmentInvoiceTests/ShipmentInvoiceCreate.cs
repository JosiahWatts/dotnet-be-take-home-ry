using RygenTakeHome.API.Entities;
using Shipments.Domain.Services;

namespace Shipments.Domain.Tests.Entities.ShipmentInvoiceTests;

public class ShipmentInvoiceCreate
{
    [Fact]
    public void CanCreateInvoice()
    {
        var shipment = new Shipment(1);

        shipment.AddStop(new ShipmentStop(1, "The White House", 0.0));
        shipment.AddStop(new ShipmentStop(2, "Bob's House", 425.0));
        shipment.AddStop(new ShipmentStop(3, "World of Coca-Cola", 123.0));

        shipment.AddLineItem(new ShipmentLineItem(3, "tigers", 10, 100.0, 1, 3));
        shipment.AddLineItem(new ShipmentLineItem(3, "lions", 10, 125.0, 1, 2));

        var invoice = ShipmentInvoice.Create(shipment, new MileageCostAllocator());

        Assert.NotNull(invoice);
    }
}

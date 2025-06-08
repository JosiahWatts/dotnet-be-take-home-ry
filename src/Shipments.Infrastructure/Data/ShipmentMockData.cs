
using RygenTakeHome.API.Entities;

namespace Shipments.Infrastructure.Data;

public class ShipmentMockData
{
    public static List<Shipment> GetShipments()
    {
        List<Shipment> shipmentList = new List<Shipment>();

        var shipment = new Shipment(1);

        shipment.AddStop(new ShipmentStop(1, "The White House", 0.0));
        shipment.AddStop(new ShipmentStop(2, "Bob's House", 425.0));
        shipment.AddStop(new ShipmentStop(3, "World of Coca-Cola", 123.0));

        shipment.AddLineItem(new ShipmentLineItem(1, "tigers", 10, 100.0, 1, 3));
        shipment.AddLineItem(new ShipmentLineItem(1, "lions", 10, 125.0, 1, 2));
        shipment.AddLineItem(new ShipmentLineItem(2, "bears", 5, 150, 2, 3));


        var anotherShipment = new Shipment(2);

        anotherShipment.AddStop(new ShipmentStop(1, "New York", 0.0));
        anotherShipment.AddStop(new ShipmentStop(2, "Somewhere 900 units away", 900.24));
        anotherShipment.AddStop(new ShipmentStop(3, "Somewhere 500 units away from 900", 500.0));
        anotherShipment.AddStop(new ShipmentStop(4, "Whoa", 1500.0));

        anotherShipment.AddLineItem(new ShipmentLineItem(1, "tigers", 10, 10.0, 1, 3));
        anotherShipment.AddLineItem(new ShipmentLineItem(1, "lions", 10, 25.0, 1, 2));
        anotherShipment.AddLineItem(new ShipmentLineItem(2, "bears", 5, 601.0, 2, 3));
        anotherShipment.AddLineItem(new ShipmentLineItem(2, "oh my", 5, 2.0, 2, 4));

        shipmentList.Add(shipment);
        shipmentList.Add(anotherShipment);

        return shipmentList;
    }
}

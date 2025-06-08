using RygenTakeHome.API.Entities;

namespace Shipments.Domain.Tests.Entities.ShipmentTests;
public class ShipmentAddLineItem
{
    private readonly long _shipmentId = 1;
    private readonly long _orderId = 1;
    private readonly string _itemDescription = "Test Item";
    private readonly int _qty = 1;
    private readonly double _weight = 100;
    private readonly int _pickupStopSequenceNumber = 1;
    private readonly int _dropOffStopSequenceNumber = 2;

    [Fact]
    public void AddLineItemToShipment()
    {
        var shipment = new Shipment(_shipmentId);

        shipment.AddStop(new ShipmentStop(1, "The White House", 0.0));
        shipment.AddStop(new ShipmentStop(2, "Bob's House", 425.0));
        shipment.AddStop(new ShipmentStop(3, "World of Coca-Cola", 123.0));

        var lineItem = new ShipmentLineItem(
            _orderId,
            _itemDescription,
            _qty,
            _weight,
            _pickupStopSequenceNumber,
            _dropOffStopSequenceNumber
        );

        shipment.AddLineItem(lineItem);

        var firstItem = shipment.ShipmentLineItems.First();

        Assert.NotNull( firstItem);
        Assert.Equal(_orderId, firstItem.OrderId);
        Assert.Equal(_itemDescription, firstItem.ItemDescription);
        Assert.Equal(_qty, firstItem.Qty);
        Assert.Equal(_weight, firstItem.Weight);
        Assert.Equal(_pickupStopSequenceNumber, firstItem.PickupStopSequenceNumber);
        Assert.Equal(_dropOffStopSequenceNumber, firstItem.DropOffStopSequenceNumber);
    }

    [Fact]
    public void CantAddLineItemsWithNoStops()
    {
        var shipment = new Shipment(_shipmentId);

        var lineItem = new ShipmentLineItem(
            _orderId,
            _itemDescription,
            _qty,
            _weight,
            _pickupStopSequenceNumber,
           _dropOffStopSequenceNumber
        );

        Assert.Throws<Exception>(() => shipment.AddLineItem(lineItem));
    }

    [Fact]
    public void CantAddLineItemWithOutOfBoundsSequence()
    {
        int outOfBoundsSequence = 5;

        var shipment = new Shipment(_shipmentId);

        shipment.AddStop(new ShipmentStop(1, "The White House", 0.0));
        shipment.AddStop(new ShipmentStop(2, "Bob's House", 425.0));
        shipment.AddStop(new ShipmentStop(3, "World of Coca-Cola", 123.0));

        var lineItem = new ShipmentLineItem(
            _orderId,
            _itemDescription,
            _qty,
            _weight,
            _pickupStopSequenceNumber,
           outOfBoundsSequence
        );

        Assert.Throws<ArgumentNullException>(() => shipment.AddLineItem(lineItem));
    }

    [Fact] 
    public void CanClearLineItems()
    {
        var shipment = new Shipment(_shipmentId);

        shipment.AddStop(new ShipmentStop(1, "The White House", 0.0));
        shipment.AddStop(new ShipmentStop(2, "Bob's House", 425.0));
        shipment.AddStop(new ShipmentStop(3, "World of Coca-Cola", 123.0));

        var lineItem = new ShipmentLineItem(
            _orderId,
            _itemDescription,
            _qty,
            _weight,
            _pickupStopSequenceNumber,
            _dropOffStopSequenceNumber
        );

        shipment.AddLineItem(lineItem);

        shipment.ClearLineItems();

        Assert.Empty(shipment.ShipmentLineItems);
    }
}

using Ardalis.GuardClauses;
using RygenTakeHome.API.Interfaces;

namespace RygenTakeHome.API.Entities;

public class Shipment
{
    private readonly List<ShipmentLineItem> _shipmentLineItems = [];
    private readonly List<ShipmentStop> _shipmentStops = [];

    public Shipment(long shipmentId)
    {
        ShipmentId = shipmentId;
    }

    public long ShipmentId { get; private set; }
    public IReadOnlyCollection<ShipmentStop> ShipmentStops => _shipmentStops.AsReadOnly();
    public IReadOnlyCollection<ShipmentLineItem> ShipmentLineItems => _shipmentLineItems.AsReadOnly();

    public Shipment AddStop(ShipmentStop stop)
    {
        Guard.Against.Null(stop);

        _shipmentStops.Add(stop);

        return this;
    }

    public Shipment AddLineItem(ShipmentLineItem lineItem)
    {
        Guard.Against.Null(lineItem);

        if (ShipmentStops.Count < 1)
        {
            throw new Exception("Cannot add ShipmentLineItem without ShipmentStops");
        }
        
        var startSeqNum = lineItem.PickupStopSequenceNumber;
        var endSeqNum = lineItem.DropOffStopSequenceNumber;

        Guard.Against.Null(
            ShipmentStops.Where(stop => stop.SequenceNumber == startSeqNum).FirstOrDefault(),
            message: "PickupStopSequenceNumber does not exist in Shipment Stops"
        );

        Guard.Against.Null(
            ShipmentStops.Where(stop => stop.SequenceNumber == endSeqNum).FirstOrDefault(),
            message: "DropOffStopSequenceNumber does not exist in Shipment Stops"
        );

        _shipmentLineItems.Add(lineItem);

        return this;
    }

    public void ClearLineItems()
    {
        _shipmentLineItems.Clear();
    }

    public void ClearStops()
    {
        _shipmentLineItems.Clear();
        _shipmentStops.Clear();
    }

    public ShipmentInvoice CreateInvoice(ICostAllocator allocator)
    {
        var invoice = ShipmentInvoice.Create(this, allocator);
        return invoice;
    }
}
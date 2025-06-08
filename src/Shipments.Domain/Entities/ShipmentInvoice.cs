using RygenTakeHome.API.Interfaces;
using Shipments.Domain;

namespace RygenTakeHome.API.Entities;

public class ShipmentInvoice
{
    public long ShipmentId { get; private set; }
    public decimal TotalCost { get; private set; }
    public CostAllocationResult? CostAllocationResult { get; private set; }

    public static ShipmentInvoice Create(Shipment shipment, ICostAllocator allocator)
    {
        var allocationResult = allocator.AllocateCosts(shipment, 0);
        var shipmentCost = allocationResult.CostsByOrderId.Values.Sum();

        return new ShipmentInvoice
        {
           ShipmentId = shipment.ShipmentId,
           TotalCost = shipmentCost,
           CostAllocationResult = allocationResult,
        };
    }   
}

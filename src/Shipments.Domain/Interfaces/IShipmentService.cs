using Ardalis.Result;
using RygenTakeHome.API.Entities;
using Shipments.Domain.Entities;

namespace Shipments.Domain.Interfaces;

public interface IShipmentService
{
    Result<Shipment> GetShipmentById(long shipmentId);
    Result<ShipmentInvoice> CreateShipmentInvoice(long shipmentId, CostAllocationMethod allocationMethod);
}

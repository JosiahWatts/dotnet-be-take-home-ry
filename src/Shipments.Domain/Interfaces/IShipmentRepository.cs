using RygenTakeHome.API.Entities;

namespace Shipments.Domain.Interfaces;

public interface IShipmentRepository
{
    Shipment? GetShipmentById(long shipmentId);
}

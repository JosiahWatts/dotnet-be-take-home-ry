using RygenTakeHome.API.Entities;
using Shipments.Domain.Interfaces;
using Shipments.Infrastructure.Data;

namespace Shipments.Infrastructure;

public class ShipmentRepository : IShipmentRepository
{
    private readonly List<Shipment> _shipments = ShipmentMockData.GetShipments();

    public Shipment? GetShipmentById(long shipmentId)
    {
        var shipment = _shipments.FirstOrDefault(s => s.ShipmentId == shipmentId);
        return shipment is null ? null : shipment;
    }
}

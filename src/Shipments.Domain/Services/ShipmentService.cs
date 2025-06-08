using Ardalis.Result;
using RygenTakeHome.API.Entities;
using RygenTakeHome.API.Interfaces;
using Shipments.Domain.Entities;
using Shipments.Domain.Interfaces;

namespace Shipments.Domain.Services;

public class ShipmentService : IShipmentService
{
    private readonly IShipmentRepository shipmentRepository;

    public ShipmentService(IShipmentRepository shipmentRepository)
    {
        this.shipmentRepository = shipmentRepository;
    }

    public Result<Shipment> GetShipmentById(long shipmentId)
    {
        var shipment = shipmentRepository.GetShipmentById(shipmentId);

        if (shipment is null)
        {
            return Result.NotFound();
        }

        return Result.Success(shipment);
    }

    // This could probably be moved over to a ShipmentInvoice service
    public Result<ShipmentInvoice> CreateShipmentInvoice(long shipmentId, CostAllocationMethod allocationMethod)
    {
        var shipment = shipmentRepository.GetShipmentById(shipmentId);

        if (shipment is null)
        {
            return Result.NotFound();
        }

        ICostAllocator allocator = allocationMethod switch
        {
            CostAllocationMethod.MILEAGE => new MileageCostAllocator(),
            CostAllocationMethod.WEIGHT => new WeightCostAllocator(),
            _ => new WeightCostAllocator(),
        };

        var invoice = shipment.CreateInvoice(allocator);

        return invoice;
    }
}

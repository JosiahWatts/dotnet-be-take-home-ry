using Ardalis.Result;
using FastEndpoints;
using RygenTakeHome.API.Entities;
using Shipments.Domain.Interfaces;

namespace Shipments.API.Endpoints.ShipmentEndpoints.GetById;

public class GetById : Endpoint<GetShipmentByIdRequest, Shipment>
{
    private readonly IShipmentService shipmentService;

    public GetById(IShipmentService shipmentService)
    {
        this.shipmentService = shipmentService;
    }
    public override void Configure()
    {
        Get("/api/shipments/{ShipmentId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetShipmentByIdRequest req, CancellationToken ct)
    {
        var result = shipmentService.GetShipmentById(req.ShipmentId);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(ct);
        }

        await SendAsync(result.Value, cancellation: ct);
    }
}

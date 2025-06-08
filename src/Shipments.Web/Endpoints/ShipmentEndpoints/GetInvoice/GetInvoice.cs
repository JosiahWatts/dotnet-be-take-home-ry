using Ardalis.Result;
using FastEndpoints;
using Shipments.Domain.Interfaces;
using System.Globalization;

namespace Shipments.API.Endpoints.ShipmentEndpoints.GetInvoice;

public class GetInvoice : Endpoint<GetShipmentInvoiceRequest, ShipmentInvoiceResponse>
{
    private readonly IShipmentService shipmentService;

    public GetInvoice(IShipmentService shipmentService)
    {
        this.shipmentService = shipmentService;
    }

    public override void Configure()
    {
        Get("/api/shipments/{ShipmentId}/invoice");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetShipmentInvoiceRequest req, CancellationToken ct)
    {
        if (req.ShipmentId < 1)
        {
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        var result = shipmentService.CreateShipmentInvoice(req.ShipmentId, req.method);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellation: ct);
            return;
        }

        var invoice = result.Value;

        var totalCost = invoice.TotalCost.ToString("C", CultureInfo.CurrentCulture);

        var allocationMethod = invoice.CostAllocationResult!.AllocationMethod;

        var allocations = invoice.CostAllocationResult.CostsByOrderId.Select(c =>
        {
            return new AllocationByOrder
            {
                OrderId = c.Key,
                OrderShipmentCost = c.Value.ToString("C", CultureInfo.CurrentCulture)
            };
        }).ToList();

        await SendAsync(new ShipmentInvoiceResponse
        {
            ShipmentId = req.ShipmentId,
            TotalCost = invoice.TotalCost.ToString("C", CultureInfo.CurrentCulture),
            CostAllocationByOrder = allocations,
            CostAllocationMethod = allocationMethod
        }, cancellation: ct);
    }
}

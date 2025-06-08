using Shipments.Domain.Entities;

namespace Shipments.API.Endpoints.ShipmentEndpoints.GetInvoice;

public record GetShipmentInvoiceRequest(long ShipmentId, CostAllocationMethod method);

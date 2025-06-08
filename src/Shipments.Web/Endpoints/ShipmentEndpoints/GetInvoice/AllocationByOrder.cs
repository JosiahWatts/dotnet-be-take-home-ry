namespace Shipments.API.Endpoints.ShipmentEndpoints.GetInvoice;

public class AllocationByOrder
{
    public long OrderId { get; set; }
    public string OrderShipmentCost { get; set; } = string.Empty;
}
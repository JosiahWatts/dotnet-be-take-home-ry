namespace Shipments.API.Endpoints.ShipmentEndpoints.GetInvoice;

public class ShipmentInvoiceResponse
{
    public long ShipmentId { get; set; }
    public string TotalCost { get; set; } = string.Empty;
    public string CostAllocationMethod { get; set; } = string.Empty;
    public List<AllocationByOrder> CostAllocationByOrder { get; set; } = [];
};

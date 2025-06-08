namespace Shipments.Domain;

public record CostAllocationResult(long ShipmentId, Dictionary<long, decimal> CostsByOrderId, string AllocationMethod);
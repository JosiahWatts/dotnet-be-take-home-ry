using RygenTakeHome.API.Entities;
using Shipments.Domain;

namespace RygenTakeHome.API.Interfaces;

public interface ICostAllocator
{
    CostAllocationResult AllocateCosts(Shipment shipment, decimal totalInvoicedAmount);
}

using RygenTakeHome.API.Entities;
using RygenTakeHome.API.Interfaces;

namespace Shipments.Domain.Services;

public class WeightCostAllocator : ICostAllocator
{
    private const decimal WEIGHT_MULTIPLIER = 3.29157m;
    private const string ALLOCATION_METHOD_LABEL = "By Weight";

    public CostAllocationResult AllocateCosts(Shipment shipment, decimal totalInvoicedAmount)
    {
        Dictionary<long, decimal> costsByOrderId = new();

        var lineItemsGroupedByOrderId = shipment.ShipmentLineItems.GroupBy(lineItem => lineItem.OrderId).ToList();

        lineItemsGroupedByOrderId.ForEach(group =>
        {
            var totalOrderWeight = group.Sum(item => item.Weight);

            var calculatedCost = ((decimal)totalOrderWeight) * WEIGHT_MULTIPLIER;

            var orderCost = decimal.Round(calculatedCost, 2, MidpointRounding.AwayFromZero);

            costsByOrderId.Add(group.Key, orderCost);
        });

        return new CostAllocationResult(shipment.ShipmentId, costsByOrderId, ALLOCATION_METHOD_LABEL);
    }
}
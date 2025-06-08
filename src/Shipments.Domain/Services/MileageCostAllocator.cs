using RygenTakeHome.API.Entities;
using RygenTakeHome.API.Interfaces;

namespace Shipments.Domain.Services;

public class MileageCostAllocator : ICostAllocator
{
    private const decimal MILEAGE_RATE = 0.12623m;
    private const string ALLOCATION_METHOD_LABEL = "By Mileage";

    public CostAllocationResult AllocateCosts(Shipment shipment, decimal totalInvoicedAmount)
    {
        var sequenceMileageDict = GetSequenceMilesDictionaryFromStops(shipment.ShipmentStops);
        
        Dictionary<long, decimal> costsByOrderId = new();

        var lineItemsGroupedByOrderId = shipment.ShipmentLineItems.GroupBy(lineItem => lineItem.OrderId).ToList();

        lineItemsGroupedByOrderId.ForEach(group =>
        {
            var groupMileage = group.Sum(item =>
            {
                double mileage = 0.0;

                var start = item.PickupStopSequenceNumber;
                var stop = item.DropOffStopSequenceNumber;

                for (int i = stop; i > start; i--)
                {
                    if (sequenceMileageDict.TryGetValue(i, out double sequenceMileage))
                    {
                        mileage += sequenceMileage;
                    }
                }

                return mileage;
            });

            var calculatedCost = (decimal)groupMileage * MILEAGE_RATE + (decimal)groupMileage;
            var normalizedCost = decimal.Round(calculatedCost, 2, MidpointRounding.ToZero);

            costsByOrderId.Add(group.Key, normalizedCost);
        });

        return new CostAllocationResult(shipment.ShipmentId, costsByOrderId, ALLOCATION_METHOD_LABEL);
    }

    private Dictionary<int, double> GetSequenceMilesDictionaryFromStops(IEnumerable<ShipmentStop> shipmentStops)
    {
        Dictionary<int, double> sequenceMileageDict = new();

        var sortedStops = shipmentStops.OrderBy(s => s.SequenceNumber).ToList();

        foreach (ShipmentStop stop in sortedStops)
        {
            if (sequenceMileageDict.ContainsKey(stop.SequenceNumber))
            {
                continue;
            }

            sequenceMileageDict.Add(stop.SequenceNumber, stop.MilesFromPreviousStop);
        }

        return sequenceMileageDict;
    }
}

using Ardalis.GuardClauses;

namespace RygenTakeHome.API.Entities;

public class ShipmentLineItem
{
    public ShipmentLineItem(
        long orderId, 
        string itemDescription, 
        int qty, 
        double weight, 
        int pickupStopSequenceNumber, 
        int dropOffStopSequenceNumber)
    {
        OrderId = Guard.Against.Negative(orderId);
        ItemDescription = Guard.Against.NullOrEmpty(itemDescription);
        Qty = Guard.Against.NegativeOrZero(qty);
        Weight = Guard.Against.NegativeOrZero(weight);

        PickupStopSequenceNumber = Guard.Against.NegativeOrZero(pickupStopSequenceNumber);
        DropOffStopSequenceNumber = Guard.Against.NegativeOrZero(dropOffStopSequenceNumber);

        if (pickupStopSequenceNumber > dropOffStopSequenceNumber)
        {
            throw new ArgumentException("pickup stop sequence cannot occur after dropoff");
        }

        if (dropOffStopSequenceNumber == pickupStopSequenceNumber) {
            throw new ArgumentException("pickup stop and dropoff stop cannot be equal");
        }
    }

    public long OrderId { get; private set; }
    public string ItemDescription { get; private set; } = string.Empty;
    public int Qty { get; private set; }
    public double Weight { get; private set; }
    public int PickupStopSequenceNumber { get; private set; }
    public int DropOffStopSequenceNumber { get; private set; }
}
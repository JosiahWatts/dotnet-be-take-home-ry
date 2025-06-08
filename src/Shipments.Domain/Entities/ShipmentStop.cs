using Ardalis.GuardClauses;

namespace RygenTakeHome.API.Entities;

public class ShipmentStop
{
    public ShipmentStop(int sequenceNumber, string address, double milesFromPreviousStop)
    {
        SequenceNumber = Guard.Against.NegativeOrZero(sequenceNumber);
        Address = Guard.Against.NullOrEmpty(address);

        if (sequenceNumber == 1)
        {
            if (milesFromPreviousStop != 0)
            {
                throw new ArgumentException("miles from previous stop must be 0 at the first sequence");
            }

            MilesFromPreviousStop = milesFromPreviousStop;
        }
        else
        {
            MilesFromPreviousStop = Guard.Against.NegativeOrZero(milesFromPreviousStop);
        }
    }

    public int SequenceNumber { get; private set; }
    public string Address { get; private set; }
    public double MilesFromPreviousStop { get; private set; }
}

#region

using Ardalis.GuardClauses;

#endregion

namespace DomainDrivenDesignExample.API.SharedKernels.ValueObjects;

public class SeatPosition : ValueObject
{
    protected SeatPosition()
    {
    }

    public SeatPosition(string row, int number)
    {
        Row = Guard.Against.NullOrWhiteSpace(row, "Row cannot be empty.")
            .ToUpper();
        Number = Guard.Against.NegativeOrZero(number, "Seat number must be positive.");
    }

    public string Row { get; }
    public int Number { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Row;
        yield return Number;
    }

    public override string ToString()
    {
        return $"{Row}{Number}";
    }
}
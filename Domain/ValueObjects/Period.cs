namespace FinTrackAPI.Domain.ValueObjects;

public record Period(int Month, int Year)
{
    public static Period FromDateTime(DateTime date) => new(date.Month, date.Year);

    public override string ToString() => $"{Month:D2}/{Year}";
}
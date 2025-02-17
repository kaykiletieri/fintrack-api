using FinTrackAPI.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinTrackAPI.Infrastructure.Persistence.Converters;

public class MoneyConverter : ValueConverter<Money, decimal>
{
    public MoneyConverter()
        : base(
            v => v.Amount,
            v => new Money(v))
    { }
}
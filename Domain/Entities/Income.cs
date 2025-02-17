using FinTrackAPI.Domain.Enums;
using FinTrackAPI.Domain.ValueObjects;

namespace FinTrackAPI.Domain.Entities;

public class Income : BaseEntity
{
    public Guid UserUuid { get; private set; }
    public User User { get; private set; }
    public IncomeType Type { get; private set; }
    public Money Amount { get; private set; }
    public DateTime Date { get; private set; }

    public Income(User user, IncomeType type, Money amount, DateTime date)
    {
        User = user;
        UserUuid = user.Uuid;
        Type = type;
        Amount = amount;
        Date = date;
    }
}
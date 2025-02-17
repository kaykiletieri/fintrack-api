using FinTrackAPI.Domain.Enums;
using FinTrackAPI.Domain.ValueObjects;

namespace FinTrackAPI.Domain.Entities;

public class Investment : BaseEntity
{
    public Guid UserUuid { get; private set; }
    public User User { get; private set; }
    public Money Amount { get; private set; }
    public InvestmentType Type { get; private set; }
    public Period Period { get; private set; }
    public bool IsCompleted { get; private set; }

    public Investment(User user, Money amount, InvestmentType type, Period period)
    {
        User = user;
        UserUuid = user.Uuid;
        Amount = amount;
        Type = type;
        Period = period;
        IsCompleted = false;
    }

    public void CompleteInvestment()
    {
        IsCompleted = true;
    }
}

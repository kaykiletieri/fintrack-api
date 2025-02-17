using FinTrackAPI.Domain.ValueObjects;

namespace FinTrackAPI.Domain.Entities;

public class EmergencyFund : BaseEntity
{
    public Guid UserUuid { get; private set; }
    public User User { get; private set; }
    public Money? TargetAmount { get; private set; }
    public Money CurrentAmount { get; private set; } = Money.Zero;
    public decimal MonthlyPercentage { get; private set; }
    public bool HasTarget => TargetAmount is not null;
    public bool IsCompleted => HasTarget && CurrentAmount >= (TargetAmount ?? Money.Zero);

    public EmergencyFund(User user, decimal monthlyPercentage, Money? targetAmount = null)
    {
        User = user;
        UserUuid = user.Uuid;
        MonthlyPercentage = monthlyPercentage;
        TargetAmount = targetAmount;
    }

    public void AddContribution(Money amount)
    {
        CurrentAmount += amount;
    }

    public Money CalculateMonthlyContribution()
    {
        return new Money(User.BaseSalary.Amount * (MonthlyPercentage / 100));
    }
}

using FinTrackAPI.Domain.ValueObjects;

namespace FinTrackAPI.Domain.Entities;

public class EmergencyFund : BaseEntity
{
    public Guid WalletUuid { get; private set; }
    public Wallet Wallet { get; private set; }
    public Money? TargetAmount { get; private set; }
    public Money CurrentAmount { get; private set; } = Money.Zero;
    public decimal MonthlyPercentage { get; private set; }
    public bool HasTarget => TargetAmount is not null;
    public bool IsCompleted => HasTarget && CurrentAmount >= (TargetAmount ?? Money.Zero);

    public EmergencyFund(Wallet wallet, decimal monthlyPercentage, Money? targetAmount = null)
    {
        Wallet = wallet;
        WalletUuid = wallet.Uuid;
        MonthlyPercentage = monthlyPercentage;
        TargetAmount = targetAmount;
    }

    public void AddContribution(Money amount)
    {
        CurrentAmount += amount;
    }

    public Money CalculateMonthlyContribution()
    {
        return new Money(Wallet.Balance.Amount * (MonthlyPercentage / 100));
    }
}

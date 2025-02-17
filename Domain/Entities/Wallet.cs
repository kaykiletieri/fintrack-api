using FinTrackAPI.Domain.ValueObjects;

namespace FinTrackAPI.Domain.Entities;

public class Wallet : BaseEntity
{
    public Guid UserUuid { get; private set; }
    public User User { get; private set; }

    public Money Balance { get; private set; } = Money.Zero;

    public EmergencyFund EmergencyFund { get; private set; }

    public List<CreditCard> CreditCards { get; private set; } = [];
    public List<Income> Incomes { get; private set; } = [];
    public List<Expense> Expenses { get; private set; } = [];
    public List<Investment> Investments { get; private set; } = [];

    public Wallet(User user, decimal emergencyPercentage, Money? emergencyTarget = null)
    {
        User = user;
        UserUuid = user.Uuid;
        EmergencyFund = new EmergencyFund(this, emergencyPercentage, emergencyTarget);
        RecalculateBalance();
    }

    public void RecalculateBalance()
    {
        Money totalIncome = Incomes.Count != 0 ? new Money(Incomes.Sum(i => i.Amount.Amount)) : Money.Zero;
        Money totalExpenses = Expenses.Count != 0 ? new Money(Expenses.Sum(e => e.Amount.Amount)) : Money.Zero;
        Money totalInvestments = Investments.Count != 0 ? new Money(Investments.Sum(i => i.Amount.Amount)) : Money.Zero;
        Money emergencyContributions = EmergencyFund?.CurrentAmount ?? Money.Zero;

        Period currentPeriod = Period.FromDateTime(DateTime.UtcNow);

        Money pendingInvoices = new(
            CreditCards.SelectMany(c => c.Invoices)
                .Where(i => !i.IsPaid && i.Period == currentPeriod)
                .Sum(i => i.TotalAmount.Amount)
        );

        Balance = totalIncome - (totalExpenses + totalInvestments + emergencyContributions + pendingInvoices);
    }


    public void AddIncome(Income income)
    {
        Incomes.Add(income);
        RecalculateBalance();
    }

    public void AddExpense(Expense expense)
    {
        Expenses.Add(expense);
        RecalculateBalance();
    }

    public void AddInvestment(Investment investment)
    {
        Investments.Add(investment);
        RecalculateBalance();
    }

    public void ContributeToEmergencyFund()
    {
        Money amount = EmergencyFund.CalculateMonthlyContribution();
        EmergencyFund.AddContribution(amount);
        RecalculateBalance();
    }

    public void AddCreditCard(CreditCard creditCard)
    {
        CreditCards.Add(creditCard);
    }
}

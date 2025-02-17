using FinTrackAPI.Domain.Enums;
using FinTrackAPI.Domain.ValueObjects;

namespace FinTrackAPI.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public Money BaseSalary { get; private set; }
    public decimal InvestmentPercentage { get; private set; }
    public EmergencyFund EmergencyFund { get; private set; }
    public List<Income> Incomes { get; private set; } = [];
    public List<Expense> Expenses { get; private set; } = [];
    public List<CreditCard> CreditCards { get; private set; } = [];
    public List<Investment> Investments { get; private set; } = [];

    public User(string name, string email, Money baseSalary, decimal investmentPercentage, decimal emergencyPercentage, Money? emergencyTarget = null)
    {
        Name = name;
        Email = email;
        BaseSalary = baseSalary;
        InvestmentPercentage = investmentPercentage;
        EmergencyFund = new EmergencyFund(this, emergencyPercentage, emergencyTarget);
    }

    public Money CalculateInvestmentAmount() =>
        new(BaseSalary.Amount * (InvestmentPercentage / 100));

    public void RegisterMonthlyInvestment(InvestmentType type, Period period)
    {
        Money amount = CalculateInvestmentAmount();
        if (amount.Amount > 0)
        {
            Investments.Add(new Investment(this, amount, type, period));
        }
    }

    public void ContributeToEmergencyFund()
    {
        Money amount = EmergencyFund.CalculateMonthlyContribution();
        EmergencyFund.AddContribution(amount);
    }
}
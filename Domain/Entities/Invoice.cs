using FinTrackAPI.Domain.ValueObjects;

namespace FinTrackAPI.Domain.Entities;

public class Invoice : BaseEntity
{
    public Guid CreditCardUuid { get; private set; }
    public CreditCard CreditCard { get; private set; }
    public Period Period { get; private set; }
    public Money TotalAmount { get; private set; } = Money.Zero;
    public bool IsPaid { get; private set; }
    public List<Expense> Expenses { get; private set; } = [];

    public Invoice(CreditCard creditCard, Period period)
    {
        CreditCard = creditCard;
        CreditCardUuid = creditCard.Uuid;
        Period = period;
        IsPaid = false;
    }

    public void AddExpense(Expense expense)
    {
        Expenses.Add(expense);
        TotalAmount += expense.Amount;
    }

    public void Pay()
    {
        IsPaid = true;
    }
}

using FinTrackAPI.Domain.ValueObjects;

namespace FinTrackAPI.Domain.Entities;

public class CreditCard : BaseEntity
{
    public string Name { get; private set; }
    public Money Limit { get; private set; }
    public List<Expense> Expenses { get; private set; } = [];
    public List<Invoice> Invoices { get; private set; } = [];
    public Guid WalletUuid { get; private set; }
    public Wallet Wallet { get; private set; }

    public CreditCard(Wallet wallet, string name, Money limit)
    {
        Wallet = wallet;
        WalletUuid = wallet.Uuid;
        Name = name;
        Limit = limit;
    }

    public void AddExpense(Expense expense)
    {
        Invoice invoice = GetOrCreateInvoice(expense.Date);
        invoice.AddExpense(expense);
        Expenses.Add(expense);
    }

    private Invoice GetOrCreateInvoice(DateTime date)
    {
        Period period = Period.FromDateTime(date);

        Invoice? existingInvoice = Invoices.FirstOrDefault(i => i.Period == period);
        if (existingInvoice != null) return existingInvoice;

        Invoice newInvoice = new(this, period);
        Invoices.Add(newInvoice);
        return newInvoice;
    }

    public Money GetTotalExpenses() => new(Expenses.Sum(e => e.Amount.Amount));
}

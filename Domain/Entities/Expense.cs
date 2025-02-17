using FinTrackAPI.Domain.Enums;
using FinTrackAPI.Domain.ValueObjects;

namespace FinTrackAPI.Domain.Entities;

public class Expense : BaseEntity
{
    public string Description { get; private set; }
    public ExpenseType Type { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public Money Amount { get; private set; }
    public DateTime Date { get; private set; }
    public Guid OwnerUuid { get; private set; }
    public User Owner { get; private set; }
    public bool IsInstallment { get; private set; }
    public int? Installments { get; private set; }
    public int? CurrentInstallment { get; private set; }
    public Guid? InvoiceUuid { get; private set; }
    public Invoice? Invoice { get; private set; }

    public Expense(User owner, string description, ExpenseType type, PaymentMethod paymentMethod, Money amount, DateTime date, Invoice? invoice = null)
    {
        Owner = owner;
        OwnerUuid = owner.Uuid;
        Description = description;
        Type = type;
        PaymentMethod = paymentMethod;
        Amount = amount;
        Date = date;
        Invoice = invoice;
        InvoiceUuid = invoice?.Uuid;
        IsInstallment = false;
    }

    public Expense(User owner, string description, ExpenseType type, PaymentMethod paymentMethod, Money amount, DateTime date, bool isInstallment, int? installments, int? currentInstallment, Invoice? invoice = null)
    {
        Owner = owner;
        OwnerUuid = owner.Uuid;
        Description = description;
        Type = type;
        PaymentMethod = paymentMethod;
        Amount = amount;
        Date = date;
        Invoice = invoice;
        InvoiceUuid = invoice?.Uuid;
        IsInstallment = isInstallment;
        Installments = installments;
        CurrentInstallment = currentInstallment;
    }

    public List<Expense> GenerateInstallments()
    {
        if (!IsInstallment || Installments == null || Installments <= 1)
            throw new InvalidOperationException("This expense is not an installment or has invalid installment count.");

        List<Expense> installmentExpenses = [];
        Money installmentAmount = new(Amount.Amount / Installments.Value);
        for (int i = 1; i <= Installments; i++)
        {
            installmentExpenses.Add(new Expense(
                Owner,
                $"{Description} (Installment {i}/{Installments})",
                Type,
                PaymentMethod,
                installmentAmount,
                Date.AddMonths(i - 1),
                true,
                Installments,
                i
            ));
        }
        return installmentExpenses;
    }
}

using FinTrackAPI.Domain.Enums;
using FinTrackAPI.Domain.ValueObjects;

namespace FinTrackAPI.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public Money BaseSalary { get; private set; }

    public User(string name, string email, Money baseSalary)
    {
        Name = name;
        Email = email;
        BaseSalary = baseSalary;
    }
}
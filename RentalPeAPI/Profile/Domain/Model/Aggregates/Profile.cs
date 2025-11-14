using RentalPeAPI.Profile.Domain.Model.Entities;
using RentalPeAPI.Profile.Domain.Model.ValueObjects;

namespace RentalPeAPI.Profile.Domain.Model.Aggregates;

/// <summary>
/// Aggregate raíz para el perfil de usuario en RentalPe.
/// </summary>
public class Profile
{
    public int Id { get; private set; }

    /// <summary>Id del usuario dueño del perfil (User BC / IAM).</summary>
    public long UserId { get; private set; }

    public string FullName { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public string Department { get; private set; } = string.Empty;
    public string PrimaryEmail { get; private set; } = string.Empty;
    public string PrimaryPhone { get; private set; } = string.Empty;

    private readonly List<PaymentMethod> _paymentMethods = new();
    public IReadOnlyCollection<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // ctor vacío para EF
    protected Profile() { }

    public Profile(
        long userId,
        string fullName,
        string country,
        string department,
        string primaryEmail,
        string primaryPhone)
    {
        UserId = userId;
        FullName = fullName;
        Country = country;
        Department = department;
        PrimaryEmail = primaryEmail;
        PrimaryPhone = primaryPhone;
    }

    public void UpdateBasicInformation(
        string fullName,
        string country,
        string department,
        string primaryEmail,
        string primaryPhone)
    {
        FullName = fullName;
        Country = country;
        Department = department;
        PrimaryEmail = primaryEmail;
        PrimaryPhone = primaryPhone;
    }

    public void AddPaymentMethod(PaymentMethod paymentMethod)
    {
        _paymentMethods.Add(paymentMethod);
    }

    public void RemovePaymentMethod(int paymentMethodId)
    {
        var method = _paymentMethods.FirstOrDefault(x => x.Id == paymentMethodId);
        if (method is not null) _paymentMethods.Remove(method);
    }
}

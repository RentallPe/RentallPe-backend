using RentalPeAPI.Profiles.Domain.Model.Commands;
using RentalPeAPI.Profiles.Domain.Model.Enums;
using RentalPeAPI.Profiles.Domain.Model.ValueObjects;

namespace RentalPeAPI.Profiles.Domain.Model.Aggregates;

public partial class Profile
{
    private readonly List<PaymentMethod> _paymentMethods = new();

    public Profile()
    {
        Name = new PersonName();
        Email = new EmailAddress();
        Phone = new PhoneNumber();
        Role = ProfileRole.Customer;
    }

    public Profile(
        string firstName,
        string lastName,
        string email,
        string password,
        string phone,
        string photo,
        ProfileRole role)
    {
        Name = new PersonName(firstName, lastName);
        Email = new EmailAddress(email);
        Password = password; // en Application/Infra deberías manejar hash
        Phone = new PhoneNumber(phone);
        Photo = photo;
        Role = role;
    }

    public Profile(CreateProfileCommand command)
    {
        Name = new PersonName(command.FirstName, command.LastName);
        Email = new EmailAddress(command.Email);
        Password = command.Password;
        Phone = new PhoneNumber(command.Phone);
        Photo = command.Photo;
        Role = command.Role;
    }

    public int Id { get; }

    public PersonName Name { get; }

    public EmailAddress Email { get; }

    /// <summary>
    /// Password en texto. En producción deberías almacenar hash.
    /// </summary>
    public string Password { get; private set; } = string.Empty;

    public PhoneNumber Phone { get; }

    public string Photo { get; private set; } = string.Empty;

    public ProfileRole Role { get; private set; }

    public IReadOnlyCollection<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

    public string FullName => Name.FullName;
    public string EmailAddress => Email.Address;
    public string PhoneNumber => Phone.Number;

    public void ChangePhoto(string photoUrl)
    {
        if (string.IsNullOrWhiteSpace(photoUrl))
            throw new ArgumentException("Photo URL cannot be empty.", nameof(photoUrl));

        Photo = photoUrl;
    }

    public void ChangeRole(ProfileRole role)
    {
        Role = role;
    }

    public void ChangePassword(string newPassword)
    {
        if (string.IsNullOrWhiteSpace(newPassword))
            throw new ArgumentException("Password cannot be empty.", nameof(newPassword));

        Password = newPassword;
    }

    public PaymentMethod AddPaymentMethod(
        string type,
        string number,
        string expiry,
        string cvv)
    {
        var paymentMethod = new PaymentMethod(type, number, expiry, cvv);
        _paymentMethods.Add(paymentMethod);
        return paymentMethod;
    }

    public void RemovePaymentMethod(long paymentMethodId)
    {
        var paymentMethod = _paymentMethods.FirstOrDefault(x => x.Id == paymentMethodId);
        if (paymentMethod is null) return;
        _paymentMethods.Remove(paymentMethod);
    }
}
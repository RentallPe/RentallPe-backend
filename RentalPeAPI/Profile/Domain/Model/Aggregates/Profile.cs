using RentalPeAPI.Profile.Domain.Model.ValueObjects;

namespace RentalPeAPI.Profile.Domain.Model.Aggregates;

public partial class Profile
{
    public int Id { get; }
    public UserId UserId { get; private set; }

    public string FullName { get; private set; }
    public string? Bio { get; private set; }

    public Avatar Avatar { get; private set; } = null!;
    public string PrimaryEmail { get; private set; }
    public Phone? PrimaryPhone { get; private set; }
    public Address? PrimaryAddress { get; private set; }

    protected Profile() 
    {
        UserId = new UserId(0);
        FullName = string.Empty;
        PrimaryEmail = string.Empty;
        Avatar = Avatar.Empty;
    }

    public Profile(UserId userId, string fullName, string primaryEmail, Avatar avatar,
                   string? bio = null, Phone? primaryPhone = null, Address? primaryAddress = null)
    {
        if (userId.Value <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
        if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("Full name required.", nameof(fullName));
        if (string.IsNullOrWhiteSpace(primaryEmail)) throw new ArgumentException("Primary email required.", nameof(primaryEmail));

        UserId = userId;
        FullName = fullName.Trim();
        PrimaryEmail = primaryEmail.Trim();
        Avatar = avatar ?? throw new ArgumentNullException(nameof(avatar));
        Bio = string.IsNullOrWhiteSpace(bio) ? null : bio.Trim();
        PrimaryPhone = primaryPhone;
        PrimaryAddress = primaryAddress;
    }

    public void UpdateName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException(nameof(fullName));
        FullName = fullName.Trim();
    }

    public void UpdateBio(string? bio) => Bio = string.IsNullOrWhiteSpace(bio) ? null : bio.Trim();

    public void UpdateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException(nameof(email));
        PrimaryEmail = email.Trim();
    }

    public void UpdatePhone(Phone? phone) => PrimaryPhone = phone;

    public void UpdateAddress(Address? address) => PrimaryAddress = address;

    public void UpdateAvatar(Avatar avatar) => Avatar = avatar ?? throw new ArgumentNullException(nameof(avatar));
}

namespace RentalPeAPI.Profile.Domain.Model.ValueObjects;

public sealed class PrivacySettings
{
    public bool IsProfilePublic { get; private set; }
    public bool ShowEmail { get; private set; }
    public bool ShowPhone { get; private set; }
    public bool ShareActivity { get; private set; }

    private PrivacySettings() { } 

    public PrivacySettings(bool isProfilePublic, bool showEmail, bool showPhone, bool shareActivity)
    {
        IsProfilePublic = isProfilePublic;
        ShowEmail = showEmail;
        ShowPhone = showPhone;
        ShareActivity = shareActivity;
    }

    public static PrivacySettings Default() => new(true, false, false, false);
}
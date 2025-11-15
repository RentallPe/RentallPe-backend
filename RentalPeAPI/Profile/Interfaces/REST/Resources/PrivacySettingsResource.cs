namespace RentalPeAPI.Profile.Interfaces.REST.Resources;

public record PrivacySettingsResource(
    bool IsProfilePublic,
    bool ShowEmail,
    bool ShowPhone,
    bool ShareActivity
);
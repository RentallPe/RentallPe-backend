namespace RentalPeAPI.Profile.Interfaces.REST.Resources;

public record NotificationPrefsResource(
    bool Email,
    bool Sms,
    bool Push,
    bool InApp
);
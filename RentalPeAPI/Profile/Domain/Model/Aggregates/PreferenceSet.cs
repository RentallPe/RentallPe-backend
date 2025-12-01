using RentalPeAPI.Profile.Domain.Model.Enums;
using RentalPeAPI.Profile.Domain.Model.ValueObjects;

namespace RentalPeAPI.Profile.Domain.Model.Aggregates;

public partial class PreferenceSet
{
    public int Id { get; }
    public UserId UserId { get; private set; }

    public LanguageCode Language { get; private set; }
    public ThemeMode Theme { get; private set; }
    public string TimeZone { get; private set; }

    public NotificationPrefs Notifications { get; private set; } = null!;
    public PrivacySettings Privacy { get; private set; } = null!;
    
    public List<long> Favorites { get; private set; } = new();          
    public List<UserId> BlockedUsers { get; private set; } = new();

    public QuietHours? QuietHours { get; private set; }

    protected PreferenceSet() 
    {
        UserId = new UserId(0);
        Language = LanguageCode.es;
        Theme = ThemeMode.System;
        TimeZone = "UTC";
        Notifications = NotificationPrefs.AllDisabled();
        Privacy = PrivacySettings.Default();
    }

    public PreferenceSet(UserId userId,
                         LanguageCode language,
                         ThemeMode theme,
                         string timeZone,
                         NotificationPrefs notifications,
                         PrivacySettings privacy,
                         QuietHours? quietHours = null)
    {
        if (userId.Value <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
        if (string.IsNullOrWhiteSpace(timeZone)) throw new ArgumentException(nameof(timeZone));

        UserId = userId;
        Language = language;
        Theme = theme;
        TimeZone = timeZone.Trim();

        Notifications = notifications ?? throw new ArgumentNullException(nameof(notifications));
        Privacy = privacy ?? throw new ArgumentNullException(nameof(privacy));
        QuietHours = quietHours;
    }
    
    public void MergeNotifications(NotificationPrefs prefs) => Notifications = prefs ?? Notifications;

    public void UpdatePrivacy(PrivacySettings privacy) => Privacy = privacy ?? Privacy;
    
    public void UpdateLanguage(LanguageCode language) => Language = language;
    public void UpdateTheme(ThemeMode theme) => Theme = theme;

    public void UpdateTimeZone(string timeZone)
    {
        if (string.IsNullOrWhiteSpace(timeZone)) throw new ArgumentException(nameof(timeZone));
        TimeZone = timeZone.Trim();
    }

    public void SetQuietHours(QuietHours quiet) => QuietHours = quiet;
    public void ClearQuietHours() => QuietHours = null;

    public void SetNotifications(NotificationPrefs prefs) => Notifications = prefs ?? Notifications;   // <-- NUEVO
    public void SetPrivacy(PrivacySettings privacy) => Privacy = privacy ?? Privacy;                   // asegúralo

    public void AddFavorite(long remodelingId)
    {
        if (!Favorites.Contains(remodelingId)) Favorites.Add(remodelingId);
    }

    public void RemoveFavorite(long remodelingId) => Favorites.Remove(remodelingId);


}

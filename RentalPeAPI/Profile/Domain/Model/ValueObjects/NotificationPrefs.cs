namespace RentalPeAPI.Profile.Domain.Model.ValueObjects;

public sealed class NotificationPrefs
{
    public bool Email { get; private set; }
    public bool Sms { get; private set; }
    public bool Push { get; private set; }
    public bool InApp { get; private set; }

    private NotificationPrefs() { } 

    public NotificationPrefs(bool email, bool sms, bool push, bool inApp)
    {
        Email = email;
        Sms = sms;
        Push = push;
        InApp = inApp;
    }

    public static NotificationPrefs AllDisabled() => new(false, false, false, false);

    public IReadOnlyCollection<string> EnabledChannels()
    {
        var list = new List<string>(4);
        if (Email) list.Add("email");
        if (Sms) list.Add("sms");
        if (Push) list.Add("push");
        if (InApp) list.Add("inApp");
        return list;
    }
}
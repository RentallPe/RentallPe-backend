namespace RentalPeAPI.Profile.Domain.Model.ValueObjects;

public sealed class Avatar
{
    public string Url { get; private set; }

    public static Avatar Empty => new(string.Empty);

    private Avatar() { Url = string.Empty; } // EF

    public Avatar(string url)
    {
        if (!string.IsNullOrWhiteSpace(url) &&
            !Uri.TryCreate(url, UriKind.Absolute, out _))
            throw new ArgumentException("Invalid avatar url.", nameof(url));

        Url = url?.Trim() ?? string.Empty;
    }
}
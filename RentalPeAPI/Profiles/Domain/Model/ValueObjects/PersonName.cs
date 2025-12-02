namespace RentalPeAPI.Profiles.Domain.Model.ValueObjects;

public record PersonName
{
    public PersonName()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public PersonName(string firstName, string lastName)
    {
        FirstName = firstName?.Trim() ?? string.Empty;
        LastName = lastName?.Trim() ?? string.Empty;
    }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string FullName => $"{FirstName} {LastName}".Trim();
}
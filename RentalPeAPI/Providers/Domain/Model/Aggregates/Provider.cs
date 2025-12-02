namespace RentalPeAPI.providers.Domain.Model.Aggregates;

public partial class Provider
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    // ✅ AHORA ES SOLO UN STRING
    public string Contact { get; private set; } = string.Empty;

    protected Provider() { }

    public Provider(string name, string contact)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));

        if (string.IsNullOrWhiteSpace(contact))
            throw new ArgumentException("Contact is required", nameof(contact));

        Name = name;
        Contact = contact;
    }

    public void Update(string name, string contact)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));

        if (string.IsNullOrWhiteSpace(contact))
            throw new ArgumentException("Contact is required", nameof(contact));

        Name = name;
        Contact = contact;
    }
}
namespace RentalPeAPI.Property.Domain.Aggregates.Entities;

public class Service
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    protected Service() { }

    public Service(string name)
    {
        Name = name;
    }
}
namespace RentalPeAPI.User.Domain;

public class AppUser
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    public string? Phone { get; private set; } // NUE 2025-11-15 Braulio
    public DateTime CreatedAt { get; private set; } // NUE 2025-11-15 Braulio
    public string Role { get; private set; } = "customer"; // NUE 2025-11-15 Braulio
    public Guid? ProviderId { get; private set; } // NUE 2025-11-15 Braulio
    public string? Photo { get; private set; } // NUE 2025-11-15 Braulio
    public List<PaymentMethod> PaymentMethods { get; private set; } = new(); // NUE 2025-11-15 Braulio

    private AppUser() { }

    public AppUser(Guid id, string fullName, string email, string passwordHash,
        string? phone = null, string role = "customer",
        Guid? providerId = null, string? photo = null)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("El email no puede estar vacío.");

        Id = id;
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        Phone = phone; // NUE 2025-11-15 Braulio
        CreatedAt = DateTime.UtcNow; // NUE 2025-11-15 Braulio
        Role = role; // NUE 2025-11-15 Braulio
        ProviderId = providerId; // NUE 2025-11-15 Braulio
        Photo = photo; // NUE 2025-11-15 Braulio
    }
}

public class PaymentMethod // NUE 2025-11-15 Braulio
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Expiry { get; set; } = string.Empty;
    public string CVV { get; set; } = string.Empty;
}
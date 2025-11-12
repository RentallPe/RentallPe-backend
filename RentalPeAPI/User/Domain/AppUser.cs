namespace RentalPeAPI.User.Domain;

public class AppUser
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }


    private AppUser() { }

    
    public AppUser(Guid id, string fullName, string email, string passwordHash)
    {
       
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("El email no puede estar vacío.");
        
        Id = id;
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
    }
}
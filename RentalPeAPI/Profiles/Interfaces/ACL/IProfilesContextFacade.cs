namespace RentalPeAPI.Profiles.Interfaces.ACL;

public interface IProfilesContextFacade
{
    /// <summary>
    /// Create a profile in Profiles BC.
    /// </summary>
    Task<int> CreateProfile(
        string firstName,
        string lastName,
        string email,
        string password,
        string phone,
        string photo,
        string role);

    /// <summary>
    /// Fetch the profile id by email.
    /// </summary>
    Task<int> FetchProfileIdByEmail(string email);
}
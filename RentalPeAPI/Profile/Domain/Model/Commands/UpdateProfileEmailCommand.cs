namespace RentalPeAPI.Profile.Domain.Model.Commands;

public sealed record UpdateProfileEmailCommand(int ProfileId, string Email);
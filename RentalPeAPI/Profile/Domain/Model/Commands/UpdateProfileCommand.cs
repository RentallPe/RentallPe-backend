namespace RentalPeAPI.Profile.Domain.Model.Commands;

public sealed record UpdateProfileNameCommand(int ProfileId, string FullName);
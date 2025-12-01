namespace RentalPeAPI.Profile.Domain.Model.Commands;

public sealed record UpdateProfileBioCommand(int ProfileId, string? Bio);
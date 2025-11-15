using RentalPeAPI.Profile.Domain.Model.ValueObjects;

namespace RentalPeAPI.Profile.Domain.Model.Commands;

public sealed record UpdateProfileAvatarCommand(int ProfileId, Avatar Avatar);
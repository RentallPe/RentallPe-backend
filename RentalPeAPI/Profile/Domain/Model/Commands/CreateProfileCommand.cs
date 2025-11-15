using RentalPeAPI.Profile.Domain.Model.ValueObjects;

namespace RentalPeAPI.Profile.Domain.Model.Commands;



public sealed record CreateProfileCommand(
    UserId UserId,
    string FullName,
    string PrimaryEmail,
    Avatar Avatar,
    string? Bio = null,
    Phone? PrimaryPhone = null,
    Address? PrimaryAddress = null);
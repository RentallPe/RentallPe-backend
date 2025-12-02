using RentalPeAPI.Profiles.Domain.Model.ValueObjects;

namespace RentalPeAPI.Profiles.Domain.Model.Queries;

public record GetProfileByEmailQuery(EmailAddress Email);
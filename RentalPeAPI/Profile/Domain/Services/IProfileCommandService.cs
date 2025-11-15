using RentalPeAPI.Profile.Domain.Model.Commands;

namespace RentalPeAPI.Profile.Domain.Services;

public interface IProfileCommandService
{
    Task<Model.Aggregates.Profile?> Handle(CreateProfileCommand command);
    Task<Model.Aggregates.Profile?> Handle(UpdateProfileNameCommand command);
    Task<Model.Aggregates.Profile?> Handle(UpdateProfileBioCommand command);
    Task<Model.Aggregates.Profile?> Handle(UpdateProfileEmailCommand command);
    Task<Model.Aggregates.Profile?> Handle(UpdateProfilePhoneCommand command);
    Task<Model.Aggregates.Profile?> Handle(UpdateProfileAddressCommand command);
    Task<Model.Aggregates.Profile?> Handle(UpdateProfileAvatarCommand command);
}
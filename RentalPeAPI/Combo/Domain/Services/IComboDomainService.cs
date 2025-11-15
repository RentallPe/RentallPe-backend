using RentalPeAPI.Combo.Domain.Aggregates.Entities;

namespace RentalPeAPI.Combo.Domain.Services;

public interface IComboDomainService
{
    bool ValidateCombo(Aggregates.Entities.Combo combo);
}
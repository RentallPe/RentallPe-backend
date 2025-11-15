namespace RentalPeAPI.Combo.Application.Internal.QueryServices;

public class ListCombosQuery
{
    public Guid? ProviderId { get; set; } // EDT 2025-11-15 Braulio


    public ListCombosQuery(Guid? providerId = null) // EDT 2025-11-15 Braulio
    {
        ProviderId = providerId;
    }
}
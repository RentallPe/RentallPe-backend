namespace RentalPeAPI.Combo.Application.Internal.QueryServices;

public class ListCombosQuery
{
    public int? ProviderId { get; set; }

    public ListCombosQuery(int? providerId = null)
    {
        ProviderId = providerId;
    }
}
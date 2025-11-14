namespace RentalPeAPI.Combo.Application.Internal.QueryServices;

public class GetComboByIdQuery
{
    public int Id { get; }

    public GetComboByIdQuery(int id)
    {
        Id = id;
    }
}
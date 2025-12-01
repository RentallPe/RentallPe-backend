namespace RentalPeAPI.Combo.Application.Internal.CommandServices;

public class DeleteComboCommand
{
    public int Id { get; }

    public DeleteComboCommand(int id)
    {
        Id = id;
    }
}
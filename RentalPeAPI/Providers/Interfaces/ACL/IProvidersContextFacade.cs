namespace RentalPeAPI.providers.Interfaces.ACL;

public interface IProvidersContextFacade
{
    /// <summary>
    /// Crea un proveedor.
    /// </summary>
    /// <param name="name">Nombre del proveedor.</param>
    /// <param name="contactEmail">Email de contacto.</param>
    /// <returns>Id del proveedor creado o 0 si falla.</returns>
    Task<int> CreateProvider(string name, string contactEmail);

    /// <summary>
    /// Obtiene un proveedor por su id.
    /// </summary>
    Task<int> FetchProviderIdByName(string name);
}
namespace RentalPeAPI.subscriptions.Interfaces.ACL;

public interface ISubscriptionsContextFacade
{
    /// <summary>
    /// Crea una suscripción para un cliente.
    /// </summary>
    /// <param name="customerId">Id del cliente.</param>
    /// <param name="plan">
    /// Plan de suscripción como string: "basic", "premium", "enterprise".
    /// </param>
    /// <param name="price">Precio del plan.</param>
    /// <param name="startDate">Fecha de inicio.</param>
    /// <param name="endDate">Fecha de fin.</param>
    /// <returns>Id de la suscripción creada o 0 si falla.</returns>
    Task<int> CreateSubscription(
        int customerId,
        string plan,
        decimal price,
        DateTimeOffset startDate,
        DateTimeOffset endDate);

    /// <summary>
    /// Obtiene el id de la suscripción activa de un cliente.
    /// </summary>
    /// <param name="customerId">Id del cliente.</param>
    /// <returns>Id de la suscripción activa o 0 si no existe.</returns>
    Task<int> FetchActiveSubscriptionIdByCustomer(int customerId);
}
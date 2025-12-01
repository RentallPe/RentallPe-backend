// Monitoring/Domain/Repositories/IIncidentRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using RentalPeAPI.Monitoring.Domain.Entities;

namespace RentalPeAPI.Monitoring.Domain.Repositories;

public interface IIncidentRepository
{
    // Crear un nuevo incidente
    Task AddAsync(Incident incident);

    // Buscar un incidente por Id (int en la BD, el front lo ve como "id")
    Task<Incident?> FindByIdAsync(int id);

    // Listar incidentes por proyecto (projectId en el JSON)
    Task<IEnumerable<Incident>> ListByProjectAsync(int projectId);
}
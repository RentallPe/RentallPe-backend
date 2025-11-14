// Monitoring/Interfaces/REST/Resources/CreateProjectResource.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RentalPeAPI.Monitoring.Interfaces.REST.Resources;

// Se usa la sintaxis de propiedad COMPLETA para permitir los atributos [Required] y [JsonPropertyName]
public record CreateProjectResource
{
    [Required] 
    [JsonPropertyName("propertyId")] 
    public long PropertyId { get; init; }

    [Required] 
    [JsonPropertyName("userId")] 
    public Guid UserId { get; init; } 

    [Required] 
    [JsonPropertyName("name")] 
    public string Name { get; init; } = default!;

    [Required] 
    [JsonPropertyName("description")] 
    public string Description { get; init; } = default!;

    [Required] 
    [JsonPropertyName("startDate")] 
    public DateTime StartDate { get; init; }

    [Required] 
    [JsonPropertyName("endDate")] 
    public DateTime EndDate { get; init; }

    // Constructor vacío (necesario para la deserialización de JSON)
    public CreateProjectResource() { } 

    // Constructor completo para mapeo (opcional)
    public CreateProjectResource(long propertyId, Guid userId, string name, string description, DateTime startDate, DateTime endDate)
    {
        PropertyId = propertyId;
        UserId = userId;
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
    }
}
// Monitoring/Application/Internal/CommandServices/CreateTaskCommand.cs
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.Monitoring.Application.Internal.CommandServices;

// El comando recibe los mismos datos del recurso
public record CreateWorkItemCommand(
    int ProjectId,
    int? IncidentId,
    int AssignedToUserId,
    string Description
) : IRequest<int>; // Devolverá el ID de la nueva tarea
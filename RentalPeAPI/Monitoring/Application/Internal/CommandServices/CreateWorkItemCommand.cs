
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.Monitoring.Application.Internal.CommandServices;


public record CreateWorkItemCommand(
    int ProjectId,
    int? IncidentId,
    int AssignedToUserId,
    string Description
) : IRequest<int>; 
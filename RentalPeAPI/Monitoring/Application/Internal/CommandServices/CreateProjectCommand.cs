
using MediatR;
using System;

namespace RentalPeAPI.Monitoring.Application.Internal.CommandServices;

public record CreateProjectCommand(
    long PropertyId,
    Guid UserId,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<int>; 
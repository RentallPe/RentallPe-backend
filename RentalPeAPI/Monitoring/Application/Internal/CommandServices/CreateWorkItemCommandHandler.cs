// Monitoring/Application/Internal/CommandServices/CreateTaskCommandHandler.cs
using MediatR;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Shared.Domain.Repositories; 
using RentalPeAPI.Monitoring.Domain.Entities; 
using System;

namespace RentalPeAPI.Monitoring.Application.Internal.CommandServices;

public class CreateWorkItemCommandHandler : IRequestHandler<CreateWorkItemCommand, int>
{
    private readonly IWorkItemRepository _workItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWorkItemCommandHandler(IWorkItemRepository workItemRepository, IUnitOfWork unitOfWork)
    {
        _workItemRepository = workItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateWorkItemCommand command, CancellationToken cancellationToken)
    {
        // 1. Crear la entidad de dominio
        var task = new WorkItem(
            command.ProjectId,
            command.IncidentId,
            command.AssignedToUserId,
            command.Description
        );

        // 2. Persistir
        await _workItemRepository.AddAsync(task);
        await _unitOfWork.CompleteAsync(); 
        
        // 3. Devolver el ID de la nueva tarea
        return task.Id; 
    }
}
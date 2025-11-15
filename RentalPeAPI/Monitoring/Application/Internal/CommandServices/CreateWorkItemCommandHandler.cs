
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
     
        var task = new WorkItem(
            command.ProjectId,
            command.IncidentId,
            command.AssignedToUserId,
            command.Description
        );

      
        await _workItemRepository.AddAsync(task);
        await _unitOfWork.CompleteAsync(); 
        
        
        return task.Id; 
    }
}
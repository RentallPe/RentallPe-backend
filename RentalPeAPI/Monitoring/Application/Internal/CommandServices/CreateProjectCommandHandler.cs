// Monitoring/Application/Internal/CommandServices/CreateProjectCommandHandler.cs
using MediatR;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Monitoring.Domain.Entities;
using RentalPeAPI.Shared.Domain.Repositories; // IUnitOfWork
using System.Threading;
using System.Threading.Tasks;
using System;

namespace RentalPeAPI.Monitoring.Application.Internal.CommandServices;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        // 1. Crear la entidad
        var project = new Project(
            command.PropertyId,
            command.UserId,
            command.Name,
            command.Description,
            command.StartDate,
            command.EndDate
        );

        // 2. Persistir
        await _projectRepository.AddAsync(project);
        await _unitOfWork.CompleteAsync();

        // 3. Devolver PK (Id)
        return project.Id;
    }
}
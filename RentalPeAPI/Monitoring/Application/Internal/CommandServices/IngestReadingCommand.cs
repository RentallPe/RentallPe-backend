// Monitoring/Application/Internal/CommandServices/IngestReadingCommand.cs
using MediatR;
using System;

namespace RentalPeAPI.Monitoring.Application.Internal.CommandServices;

// IRequest indica que este comando se ejecuta y no devuelve un valor (fire and forget)
public record IngestReadingCommand(
    int ProjectId,
    int IoTDeviceId,
    string MetricName,
    decimal Value,
    string Unit,
    DateTime Timestamp
) : IRequest;
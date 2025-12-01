
using MediatR;
using System;

namespace RentalPeAPI.Monitoring.Application.Internal.CommandServices;


public record IngestReadingCommand(
    int ProjectId,
    int IoTDeviceId,
    string MetricName,
    decimal Value,
    string Unit,
    DateTime Timestamp
) : IRequest;
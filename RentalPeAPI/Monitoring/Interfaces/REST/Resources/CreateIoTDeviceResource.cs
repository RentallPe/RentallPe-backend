// Monitoring/Interfaces/REST/Resources/CreateIoTDeviceResource.cs
namespace RentalPeAPI.Monitoring.Interfaces.REST.Resources;

public record CreateIoTDeviceResource(
    int ProjectId,
    string Name,
    string SerialNumber,
    string Type
);
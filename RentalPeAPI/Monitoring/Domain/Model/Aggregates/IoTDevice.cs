using System;

namespace RentalPeAPI.Monitoring.Domain.Model.Aggregates;

public class IoTDevice
{
    public long Id { get; private set; }
    public long ProjectId { get; private set; }

    public string Name { get; private set; } = string.Empty;      // columna NOT NULL en MySQL
    public string SerialNumber { get; private set; } = string.Empty;

    public string Type { get; private set; } = string.Empty;      // "Temperature Sensor"
    public string Status { get; private set; } = "active";        // igual que dbjson
    public DateTime InstalledAt { get; private set; } = DateTime.UtcNow;

    // EF
    private IoTDevice() { }

    public IoTDevice(long projectId, string type, string? name, string? serialNumber)
    {
        if (projectId <= 0) throw new ArgumentException("ProjectId debe ser > 0", nameof(projectId));
        if (string.IsNullOrWhiteSpace(type)) throw new ArgumentException("Type es obligatorio", nameof(type));

        ProjectId = projectId;
        Type = type;
        Name = string.IsNullOrWhiteSpace(name) ? type : name;   // si no mandas name, usamos type
        SerialNumber = serialNumber ?? string.Empty;
        Status = "active";
        InstalledAt = DateTime.UtcNow;
    }
}
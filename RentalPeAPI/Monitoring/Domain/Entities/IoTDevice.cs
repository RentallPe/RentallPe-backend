// Monitoring/Domain/Entities/IoTDevice.cs
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalPeAPI.Monitoring.Domain.Entities;

public class IoTDevice
{
    // Clave Primaria
    public int Id { get; set; } 

    // CLAVE FORÁNEA: Conexión al Project (debes tener la tabla 'project' para esto)
    public int ProjectId { get; set; } 

    // Atributos del Dispositivo
    public string Name { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; 
    public string Status { get; set; } = "Active"; 
    public DateTime InstallationDate { get; set; } = DateTime.UtcNow;

    public IoTDevice() { }

    // Constructor de creación
    public IoTDevice(int projectId, string name, string serialNumber, string type)
    {
        ProjectId = projectId;
        Name = name;
        SerialNumber = serialNumber;
        Type = type;
        Status = "Active";
        InstallationDate = DateTime.UtcNow;
    }
}
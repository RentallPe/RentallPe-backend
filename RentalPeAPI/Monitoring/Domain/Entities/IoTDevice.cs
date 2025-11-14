
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalPeAPI.Monitoring.Domain.Entities;

public class IoTDevice
{
    
    public int Id { get; set; } 

   
    public int ProjectId { get; set; } 

    
    public string Name { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; 
    public string Status { get; set; } = "Active"; 
    public DateTime InstallationDate { get; set; } = DateTime.UtcNow;

    public IoTDevice() { }

    
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
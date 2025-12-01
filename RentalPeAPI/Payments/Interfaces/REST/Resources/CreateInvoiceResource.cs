using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.Payments.Interfaces.REST.Resources;

public record CreateInvoiceResource(
    [Required] int PaymentId,
    [Required] int BookingId,
    [Required] int UserId,
    [Required] MoneyResource Total);
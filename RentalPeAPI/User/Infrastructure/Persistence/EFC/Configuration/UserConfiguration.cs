using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalPeAPI.User.Domain; 

namespace RentalPeAPI.User.Infrastructure.Persistence.EFC.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullName).IsRequired().HasMaxLength(100);
        
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.HasIndex(u => u.Email).IsUnique(); 

        builder.Property(u => u.PasswordHash).IsRequired();
        
        // Nuevas propiedades
        builder.Property(u => u.Phone).HasMaxLength(20); // NUE 2025-11-15 Braulio
        builder.Property(u => u.CreatedAt).IsRequired(); // NUE 2025-11-15 Braulio
        builder.Property(u => u.Role).HasMaxLength(50); // NUE 2025-11-15 Braulio
        builder.Property(u => u.ProviderId); // NUE 2025-11-15 Braulio
        builder.Property(u => u.Photo).HasMaxLength(250); // NUE 2025-11-15 Braulio
        
        // Configuración de PaymentMethods como colección propia
        builder.OwnsMany(u => u.PaymentMethods, pm =>
        {
            pm.ToTable("UserPaymentMethods"); // NUE 2025-11-15 Braulio
            pm.HasKey(p => p.Id); // NUE 2025-11-15 Braulio
            pm.Property(p => p.Type).IsRequired(); // NUE 2025-11-15 Braulio
            pm.Property(p => p.Number).IsRequired(); // NUE 2025-11-15 Braulio
            pm.Property(p => p.Expiry).IsRequired(); // NUE 2025-11-15 Braulio
            pm.Property(p => p.CVV).IsRequired(); // NUE 2025-11-15 Braulio
        });


        
    }
}
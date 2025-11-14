using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RentalPeAPI.Profile.Domain.Model.Aggregates;

namespace RentalPeAPI.Profile.Infrastructure.Persistence.EFC.Configuration.Extensions
{
    /// <summary>
    ///     Métodos de extensión para configurar el bounded context de Profile.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        ///     Aplica la configuración del contexto Profile al ModelBuilder.
        ///     Se llama desde AppDbContext.OnModelCreating().
        /// </summary>
        /// <param name="builder">ModelBuilder de EF Core.</param>
        public static void ApplyProfilesConfiguration(this ModelBuilder builder)
        {
            // Aggregate Profile
            var profile = builder.Entity<Domain.Model.Aggregates.Profile>();

            profile.HasKey(p => p.Id);

            profile.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            profile.Property(p => p.UserId)
                .IsRequired();

            profile.Property(p => p.FullName)
                .IsRequired()
                .HasMaxLength(80);

            profile.Property(p => p.Country)
                .IsRequired()
                .HasMaxLength(60);

            profile.Property(p => p.Department)
                .IsRequired()
                .HasMaxLength(60);

            profile.Property(p => p.PrimaryEmail)
                .IsRequired()
                .HasMaxLength(120);

            profile.Property(p => p.PrimaryPhone)
                .HasMaxLength(30);

            // Colección de métodos de pago usando el backing field _paymentMethods
            profile.Navigation(p => p.PaymentMethods)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            // Si luego quieres configurar PaymentMethod como entidad separada,
            // lo puedes hacer aquí con builder.Entity<PaymentMethod>()...
        }
    }
}
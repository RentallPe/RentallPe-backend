using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalPeAPI.providers.Domain.Model.Aggregates;

namespace RentalPeAPI.Providers.Infrastructure.Persistence.EFC.Configuration
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable("providers");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(100);

           
            builder.Property(p => p.Contact)
                .HasColumnName("contact_email")
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
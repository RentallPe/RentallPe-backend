using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Profiles.Domain.Model.Aggregates;

namespace RentalPeAPI.Profiles.Infrastructure.Persistence.EFC.configuration.extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProfilesConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Profile>(profile =>
        {
            profile.HasKey(p => p.Id);
            profile.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            // Name VO
            profile.OwnsOne(p => p.Name, name =>
            {
                // IMPORTANTE: usar la misma FK/PK que Profile
                name.WithOwner().HasForeignKey("Id");

                name.Property(n => n.FirstName).HasColumnName("first_name");
                name.Property(n => n.LastName).HasColumnName("last_name");
            });

            // Email VO
            profile.OwnsOne(p => p.Email, email =>
            {
                // Igual que arriba
                email.WithOwner().HasForeignKey("Id");

                email.Property(e => e.Address).HasColumnName("email");
            });

            // Phone VO
            profile.OwnsOne(p => p.Phone, phone =>
            {
                // Igual
                phone.WithOwner().HasForeignKey("Id");

                phone.Property(pn => pn.Number).HasColumnName("phone");
            });

            profile.Property(p => p.Password)
                .HasColumnName("password")
                .IsRequired();

            profile.Property(p => p.Photo)
                .HasColumnName("photo")
                .HasMaxLength(512);

            profile.Property(p => p.Role)
                .HasColumnName("role")
                .HasConversion<string>();

            // PaymentMethods como colección owned en otra tabla
            profile.OwnsMany(p => p.PaymentMethods, pm =>
            {
                pm.WithOwner().HasForeignKey("profile_id");

                pm.ToTable("payment_methods");

                pm.Property<long>("Id")
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                pm.HasKey("Id");

                pm.Property(p => p.Type)
                    .HasColumnName("type")
                    .IsRequired()
                    .HasMaxLength(64);

                pm.Property(p => p.Number)
                    .HasColumnName("number")
                    .IsRequired()
                    .HasMaxLength(32);

                pm.Property(p => p.Expiry)
                    .HasColumnName("expiry")
                    .IsRequired()
                    .HasMaxLength(8);

                pm.Property(p => p.Cvv)
                    .HasColumnName("cvv")
                    .IsRequired()
                    .HasMaxLength(8);
            });

            profile.Navigation(nameof(Profile.PaymentMethods))
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalPeAPI.subscriptions.Domain.Model.Aggregates;

namespace RentalPeAPI.subscriptions.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySubscriptionsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Subscription>(ConfigureSubscription);
    }

    private static void ConfigureSubscription(EntityTypeBuilder<Subscription> b)
    {
        b.HasKey(s => s.Id);
        b.Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();

        b.Property(s => s.CustomerId).IsRequired();

        b.Property(s => s.Plan)
            .HasConversion<int>()
            .IsRequired();

        b.Property(s => s.Status)
            .HasConversion<int>()
            .IsRequired();

        b.HasIndex(s => s.CustomerId);
        b.HasIndex(s => s.Status);

        // Price (VO)
        b.OwnsOne(s => s.Price, price =>
        {
            price.WithOwner().HasForeignKey("Id");
            price.Property(p => p.Amount)
                .HasColumnName("PriceAmount")
                .HasPrecision(18, 2)
                .IsRequired();
        });

        // Period (VO)
        b.OwnsOne(s => s.Period, period =>
        {
            period.WithOwner().HasForeignKey("Id");
            period.Property(p => p.StartDate)
                .HasColumnName("StartDate")
                .IsRequired();
            period.Property(p => p.EndDate)
                .HasColumnName("EndDate")
                .IsRequired();
        });

        b.Navigation(s => s.Price).IsRequired();
        b.Navigation(s => s.Period).IsRequired();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalPeAPI.Payments.Domain.Model.Aggregates;

namespace RentalPeAPI.Payments.Infrastructure.Persistence.EFC.configuration.extensions;


public static class ApplyPaymentsConfigurationExtension
{
    public static void ApplyPaymentsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Domain.Model.Aggregates.Payment>(ConfigurePayment);
        builder.Entity<Invoice>(ConfigureInvoice);
    }

    private static void ConfigurePayment(EntityTypeBuilder<Domain.Model.Aggregates.Payment> b)
    {
        b.HasKey(p => p.Id);
        b.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

        b.Property(p => p.UserId).IsRequired();
        b.Property(p => p.Status).HasConversion<int>().IsRequired();
        b.Property(p => p.Reference).HasMaxLength(100);

        b.HasIndex(p => p.UserId);
        b.HasIndex(p => p.Status);
        b.HasIndex(p => p.Reference);

        b.OwnsOne(p => p.Money, m =>
        {
            m.WithOwner().HasForeignKey("Id");
            m.Property(x => x.Amount).HasPrecision(18, 2).IsRequired();
            m.Property(x => x.Currency).HasConversion<int>().IsRequired();
        });

        b.OwnsOne(p => p.Method, m =>
        {
            m.WithOwner().HasForeignKey("Id");
            m.Property(x => x.Type).HasConversion<int>().IsRequired();
            m.Property(x => x.Label).HasMaxLength(100);
            m.Property(x => x.Last4).HasMaxLength(4);
        });

        b.Navigation(p => p.Money).IsRequired();
        b.Navigation(p => p.Method).IsRequired();
    }

    private static void ConfigureInvoice(EntityTypeBuilder<Invoice> b)
    {
        b.HasKey(i => i.Id);
        b.Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();

        b.Property(i => i.PaymentId).IsRequired();
        b.Property(i => i.BookingId).IsRequired();
        b.Property(i => i.UserId).IsRequired();
        b.Property(i => i.IssueDate).IsRequired();
        b.Property(i => i.Status).HasConversion<int>().IsRequired();

        b.HasIndex(i => i.UserId);
        b.HasIndex(i => i.PaymentId).IsUnique();

        b.HasOne<Domain.Model.Aggregates.Payment>()
            .WithOne()
            .HasForeignKey<Invoice>(i => i.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);

        b.OwnsOne(i => i.Total, m =>
        {

            m.WithOwner().HasForeignKey("Id");
            m.Property(x => x.Amount).HasPrecision(18, 2).IsRequired();
            m.Property(x => x.Currency).HasConversion<int>().IsRequired();
        });

        b.Navigation(i => i.Total).IsRequired();
    }
}
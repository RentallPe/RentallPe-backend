using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalPeAPI.Profile.Domain.Model.Aggregates;

namespace RentalPeAPI.Profile.Infrastructure.Persistence.EFC.configuration.extensions;

public static class ApplyProfilesConfigurationExtension
{
    public static void ApplyProfilesConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Domain.Model.Aggregates.Profile>(ConfigureProfile);
        builder.Entity<PreferenceSet>(ConfigurePreferenceSet);
    }

    private static void ConfigureProfile(EntityTypeBuilder<Domain.Model.Aggregates.Profile> b)
{
    b.HasKey(p => p.Id);
    b.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

    b.Property(p => p.FullName).IsRequired().HasMaxLength(200);
    b.Property(p => p.Bio).HasMaxLength(1000);
    b.Property(p => p.PrimaryEmail).IsRequired().HasMaxLength(320);

    // UserId (owned) + índice único sobre su Value
    b.OwnsOne(p => p.UserId, u =>
    {
        u.WithOwner().HasForeignKey("Id");
        u.Property(x => x.Value).HasColumnName("user_id").IsRequired();
        u.HasIndex(x => x.Value).IsUnique();     // <-- aquí
    });

    b.OwnsOne(p => p.Avatar, a =>
    {
        a.WithOwner().HasForeignKey("Id");
        a.Property(x => x.Url).HasColumnName("avatar_url").HasMaxLength(1024);
    });

    b.OwnsOne(p => p.PrimaryPhone, ph =>
    {
        ph.WithOwner().HasForeignKey("Id");
        ph.Property(x => x.Number).HasColumnName("primary_phone").HasMaxLength(32);
    });

    b.OwnsOne(p => p.PrimaryAddress, a =>
    {
        a.WithOwner().HasForeignKey("Id");
        a.Property(x => x.Line1).HasColumnName("address_line1").HasMaxLength(200);
        a.Property(x => x.Line2).HasColumnName("address_line2").HasMaxLength(200);
        a.Property(x => x.District).HasColumnName("address_district").HasMaxLength(100);
        a.Property(x => x.City).HasColumnName("address_city").HasMaxLength(100);
        a.Property(x => x.State).HasColumnName("address_state").HasMaxLength(100);
        a.Property(x => x.PostalCode).HasColumnName("address_postal_code").HasMaxLength(32);
        a.Property(x => x.Country).HasColumnName("address_country").HasMaxLength(100);
    });
}

private static void ConfigurePreferenceSet(EntityTypeBuilder<PreferenceSet> b)
{
    b.HasKey(p => p.Id);
    b.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

    b.Property(p => p.Language).HasConversion<int>().IsRequired();
    b.Property(p => p.Theme).HasConversion<int>().IsRequired();
    b.Property(p => p.TimeZone).IsRequired().HasMaxLength(100);

    // UserId (owned) + índice único
    b.OwnsOne(p => p.UserId, u =>
    {
        u.WithOwner().HasForeignKey("Id");
        u.Property(x => x.Value).HasColumnName("user_id").IsRequired();
        u.HasIndex(x => x.Value).IsUnique();     // <-- aquí
    });

    b.OwnsOne(p => p.Notifications, n =>
    {
        n.WithOwner().HasForeignKey("Id");
        n.Property(x => x.Email).HasColumnName("notify_email");
        n.Property(x => x.Sms).HasColumnName("notify_sms");
        n.Property(x => x.Push).HasColumnName("notify_push");
        n.Property(x => x.InApp).HasColumnName("notify_in_app");
    });

    b.OwnsOne(p => p.Privacy, pr =>
    {
        pr.WithOwner().HasForeignKey("Id");
        pr.Property(x => x.IsProfilePublic).HasColumnName("privacy_is_profile_public");
        pr.Property(x => x.ShowEmail).HasColumnName("privacy_show_email");
        pr.Property(x => x.ShowPhone).HasColumnName("privacy_show_phone");
        pr.Property(x => x.ShareActivity).HasColumnName("privacy_share_activity");
    });

    b.OwnsOne(p => p.QuietHours, q =>
    {
        q.WithOwner().HasForeignKey("Id");
        q.Property(x => x.Start).HasColumnName("quiet_start")
            .HasConversion(v => v.ToTimeSpan(), v => TimeOnly.FromTimeSpan(v));
        q.Property(x => x.End).HasColumnName("quiet_end")
            .HasConversion(v => v.ToTimeSpan(), v => TimeOnly.FromTimeSpan(v));
    });

    b.Ignore(p => p.Favorites);
    b.Ignore(p => p.BlockedUsers);
}
}
using HotelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configrations
{
    internal class ReservationConfigration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(r => r.Id).HasColumnName("ReservationId");

            builder.Property(r => r.TotalPrice).HasColumnType("decimal(10,2)");

            builder.Property(r => r.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(r => r.GuestFullName).IsRequired().HasMaxLength(150);
            builder.Property(r => r.GuestEmail).IsRequired().HasMaxLength(150);
            builder.Property(r => r.GuestPhone).IsRequired().HasMaxLength(30);

            builder.HasMany(r => r.Payments)
                .WithOne(p => p.Reservation)
                .HasForeignKey(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

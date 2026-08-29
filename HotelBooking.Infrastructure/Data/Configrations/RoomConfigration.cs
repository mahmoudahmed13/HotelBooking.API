using HotelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configrations
{
    internal class RoomConfigration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(r => r.Id).HasColumnName("RoomNumber");

            builder.Property(r => r.RoomType).IsRequired()
                .HasMaxLength(50);
            builder.Property(r => r.DailyRate)
                .HasColumnType("decimal(10,2)");

            // Unique per hotel, not globally - "Room 101" can exist in two different hotels.
            builder.HasIndex(r => new { r.HotelId, r.RoomNumber }).IsUnique();

            builder.Property(r => r.RowVersion).IsRowVersion();

        }
    }
}

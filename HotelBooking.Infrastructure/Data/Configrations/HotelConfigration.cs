using HotelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configrations
{
    internal class HotelConfigration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.Property(e => e.Id).HasColumnName("HotelId");

            builder.Property(h => h.Name).IsRequired().HasMaxLength(150);
            builder.Property(h => h.Address).IsRequired().HasMaxLength(250);
            builder.Property(h => h.City).IsRequired().HasMaxLength(100);
            builder.Property(h => h.ContactNumber).IsRequired().HasMaxLength(30);

            builder.HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

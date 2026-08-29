using HotelBooking.Domain.Entities.Enums;

namespace HotelBooking.Domain.Entities
{
    public class Room : BaseEntity<int>
    {
        public RoomType RoomType { get; set; }
        public string RoomNumber { get; set; } = default!;
        public int Capacity { get; set; }
        public decimal DailyRate { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public int HotelId { get; set; }
        public ICollection<ReservationRoom> ReservationRooms { get; set; } = [];


        // Optimistic concurrency token (SQL Server ROWVERSION column).
        // SQL Server auto-increments this on every UPDATE to this row.
        // EF Core includes the value it originally read in the WHERE clause of
        // any UPDATE it generates for this row - if someone else changed the
        // row in between, the WHERE matches 0 rows and EF Core throws
        // DbUpdateConcurrencyException instead of silently overwriting.
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        // Deliberately no "IsAvailable" flag here.
        // Availability is a query, not a stored fact: check ReservationRooms/Reservation
    }
}

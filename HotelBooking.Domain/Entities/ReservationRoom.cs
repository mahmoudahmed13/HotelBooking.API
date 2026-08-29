namespace HotelBooking.Domain.Entities
{
    public class ReservationRoom : BaseEntity<int>
    {
        public Room Room { get; set; } = default!;
        public int RoomId { get; set; }

        public Reservation Reservation { get; set; } = default!;
        public int ReservationId { get; set; }
    }
}
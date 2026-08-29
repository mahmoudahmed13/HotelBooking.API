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
    }
}

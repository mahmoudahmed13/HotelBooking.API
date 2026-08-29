namespace HotelBooking.Domain.Entities
{
    public class Hotel : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public string ContactNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string City { get; set; } = default!;
        public int StartRating { get; set; }
        
        public ICollection<Room> Rooms { get; set; } =[];
    }
}

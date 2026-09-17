namespace HotelBooking.UseCases.Hotels.DTOs
{
    public class GetHotelByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string ContactNumber { get; set; } = default!;
        public int StartRating { get; set; } //StarRating
        public int TotalRoomsCount { get; set; }
    }
}

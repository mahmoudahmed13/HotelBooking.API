namespace HotelBooking.UseCases.Rooms.Specifications
{
    public class RoomQueryParams
    {
        public int? HotelId { get; set; }
        public string? SearchValue { get; set; }
        public RoomSortingOptions Sort { get; set; }

        public int PageIndex { get; set; } = 1;

        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;

        private int pageSize = DefaultPageSize;
        
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize : (value < 1 ? DefaultPageSize : value);
        }
    }
}

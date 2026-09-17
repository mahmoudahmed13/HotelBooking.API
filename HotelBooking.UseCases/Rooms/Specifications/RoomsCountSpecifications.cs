using Ardalis.Specification;
using HotelBooking.Domain.Entities;

namespace HotelBooking.UseCases.Rooms.Specifications
{
    public class RoomsCountSpecifications : Specification<Room>
    {
        public RoomsCountSpecifications(RoomQueryParams queryParams)
        {
            RoomsWithHotelSpec.ApplyFilters(Query, queryParams);
        }
    }
}

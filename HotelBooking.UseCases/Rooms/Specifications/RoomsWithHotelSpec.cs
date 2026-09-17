using Ardalis.Specification;
using HotelBooking.Domain.Entities;

namespace HotelBooking.UseCases.Rooms.Specifications
{
    public class RoomsWithHotelSpec : Specification<Room>
    {
        public RoomsWithHotelSpec(int id)
        {
            Query.AsNoTracking();
            Query.Where(r => r.Id == id)
                .Include(r => r.Hotel)
                .Include(r => r.ReservationRooms)
                .ThenInclude(r => r.Reservation);
        }
        public RoomsWithHotelSpec(RoomQueryParams queryParams)
        {
            Query.AsNoTracking();

            ApplyFilters(Query, queryParams);

            Query.Include(r => r.Hotel)
            .Include(r => r.ReservationRooms)
                .ThenInclude(rr => rr.Reservation);

            switch (queryParams.Sort)
            {
                case RoomSortingOptions.NumberAsc:
                    Query.OrderBy(p => p.RoomNumber);
                    break;
                case RoomSortingOptions.NumberDesc:
                    Query.OrderByDescending(p => p.RoomNumber);
                    break;
                default:
                    Query.OrderBy(p => p.Id);
                    break;
            }

            var skip = (queryParams.PageIndex - 1) * queryParams.PageSize;
            Query.Skip(skip).Take(queryParams.PageSize);
        }

        public static void ApplyFilters(ISpecificationBuilder<Room> query, RoomQueryParams queryParams)
        {
            if (queryParams.HotelId.HasValue)
                query.Where(r => r.HotelId == queryParams.HotelId);

            if (string.IsNullOrWhiteSpace(queryParams.SearchValue))
                return;

            var search = queryParams.SearchValue.Trim();
            query.Where(r =>
                r.RoomNumber.Contains(search) ||
                r.Hotel.Name.Contains(search));
        }
        
        
    }
}

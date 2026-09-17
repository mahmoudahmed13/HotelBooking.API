using Ardalis.Specification;
using HotelBooking.Domain.Entities;

namespace HotelBooking.UseCases.ReservationRooms.Specifications
{
    public class ReservationRoomsSpecification : Specification<ReservationRoom>
    {
        public ReservationRoomsSpecification(int? reservationId, int? roomId, string? searchValue)
        {
            if (reservationId.HasValue)
                Query.Where(rr => rr.ReservationId == reservationId.Value);

            if (roomId.HasValue)
                Query.Where(rr => rr.RoomId == roomId.Value);

            if (!string.IsNullOrWhiteSpace(searchValue))
                Query.Search(rr => rr.Room.RoomNumber, $"%{searchValue}%")
                .Search(rr => rr.Reservation.GuestFullName, $"%{searchValue}%");

            Query.Include(rr => rr.Room).Include(rr => rr.Reservation);
        }
    }
}

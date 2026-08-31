using HotelBooking.Domain.Common;
using HotelBooking.UseCases.ReservationRooms.DTOs;

namespace HotelBooking.UseCases.ReservationRooms.Queries
{
    public class GetAllReservationRoomsQuery(IReservationRoomQueryService reservationRoomQueryService)
    {
        public async Task<Result<IReadOnlyList<GetAllReservationRoomsResponse>>> GetAllReservationRoomsAsync(CancellationToken ct = default)
           => Result<IReadOnlyList<GetAllReservationRoomsResponse>>
            .Ok(await reservationRoomQueryService.GetAllReservationRoomsAsync(ct));

    }
}

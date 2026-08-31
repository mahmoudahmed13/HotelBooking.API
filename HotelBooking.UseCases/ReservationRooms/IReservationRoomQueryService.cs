using HotelBooking.UseCases.ReservationRooms.DTOs;

namespace HotelBooking.UseCases.ReservationRooms
{
    public interface IReservationRoomQueryService
    {
        Task<IReadOnlyList<GetAllReservationRoomsResponse>> GetAllReservationRoomsAsync(CancellationToken ct = default);
    }
}

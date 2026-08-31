using HotelBooking.Domain.Common;
using HotelBooking.UseCases.Rooms.DTOs;

namespace HotelBooking.UseCases.Rooms
{
    public interface IRoomQueryService
    {
        Task<IReadOnlyList<GetAllRoomsResponse>> GetAllRoomsAsync(CancellationToken ct = default);
        Task<GetRoomByIdResponse?> GetRoomByIdAsync(int Id, CancellationToken ct = default);
    }
}

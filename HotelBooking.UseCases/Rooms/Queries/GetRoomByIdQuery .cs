using HotelBooking.Domain.Common;
using HotelBooking.UseCases.Rooms.DTOs;

namespace HotelBooking.UseCases.Rooms.Queries
{
    public class GetRoomByIdQuery(IRoomQueryService roomQueryService)
    {
        public async Task<Result<GetRoomByIdResponse>> GetRoomByIdAsync(int id, CancellationToken ct)
        {
            var room = await roomQueryService.GetRoomByIdAsync(id, ct);
            if (room == null)
                return Error.NotFound("Room not found.");
            return Result<GetRoomByIdResponse>.Ok(room);
        }
    }
}

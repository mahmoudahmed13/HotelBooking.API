using HotelBooking.Domain.Common;
using HotelBooking.UseCases.Rooms.DTOs;

namespace HotelBooking.UseCases.Rooms.Queries
{
    public class GetAllRoomsQuery(IRoomQueryService roomQueryService)
    {
        public async Task<Result<IReadOnlyList<GetAllRoomsResponse>>> GetAllRoomsAsync(CancellationToken ct = default)
        {
            var rooms = await roomQueryService.GetAllRoomsAsync(ct);
            return Result<IReadOnlyList<GetAllRoomsResponse>>.Ok(rooms);
        }
    }
}

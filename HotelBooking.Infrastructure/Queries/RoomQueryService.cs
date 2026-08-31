using HotelBooking.Domain.Common;
using HotelBooking.Infrastructure.Data;
using HotelBooking.UseCases.Rooms;
using HotelBooking.UseCases.Rooms.DTOs;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Queries
{
    public class RoomQueryService(AppDbContext dbContext) : IRoomQueryService
    {
        public async Task<IReadOnlyList<GetAllRoomsResponse>> GetAllRoomsAsync(CancellationToken ct = default)
        {
            return await dbContext.Rooms
                .AsNoTracking()
                .ProjectToType<GetAllRoomsResponse>()
                .ToListAsync();
        }

        public async Task<GetRoomByIdResponse?> GetRoomByIdAsync(int Id, CancellationToken ct = default)
        {
            return await dbContext.Rooms.AsNoTracking()
                .Where(r => r.Id == Id)
                .ProjectToType<GetRoomByIdResponse>()
                .FirstOrDefaultAsync(ct);
        }
    }
}

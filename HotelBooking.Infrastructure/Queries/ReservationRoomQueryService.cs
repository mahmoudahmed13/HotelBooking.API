using HotelBooking.Infrastructure.Data;
using HotelBooking.UseCases.ReservationRooms;
using HotelBooking.UseCases.ReservationRooms.DTOs;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Queries
{
    public class ReservationRoomQueryService(AppDbContext dbContext) : IReservationRoomQueryService
    {
        public async Task<IReadOnlyList<GetAllReservationRoomsResponse>> GetAllReservationRoomsAsync(CancellationToken ct = default)
        {
            return await dbContext.ReservationRooms
                .AsNoTracking()
                .ProjectToType<GetAllReservationRoomsResponse>()
                .ToListAsync();
        }
    }
}

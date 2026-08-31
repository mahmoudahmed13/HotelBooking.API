using HotelBooking.Infrastructure.Data;
using HotelBooking.UseCases.ReservationRooms.DTOs;
using HotelBooking.UseCases.Reservations;
using HotelBooking.UseCases.Reservations.DTOs;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Queries
{
    public class ReservationQueryService(AppDbContext dbContext) : IReservationQueryService
    {
        public async Task<IReadOnlyList<GetAllReservationsResponse>> GetAllReservationsAsync(CancellationToken ct = default)
        {
            return await dbContext.Reservations
                .AsNoTracking()
                .ProjectToType<GetAllReservationsResponse>()
                .ToListAsync();
        }

        public async Task<GetReservationByIdResponse?> GetReservationByIdAsync(int Id, CancellationToken ct = default)
        {
            return await dbContext.Reservations
                .AsNoTracking()
                .Where(r => r.Id == Id)
                .ProjectToType<GetReservationByIdResponse>()
                .FirstOrDefaultAsync();
        }

      
    }
}

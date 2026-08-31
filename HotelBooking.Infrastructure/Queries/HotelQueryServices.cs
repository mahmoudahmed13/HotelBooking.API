using HotelBooking.Infrastructure.Data;
using HotelBooking.UseCases.Hotels;
using HotelBooking.UseCases.Hotels.DTOs;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Queries
{
    public class HotelQueryServices(AppDbContext dbContext) : IHotelQueryService
    {
        public async Task<IReadOnlyList<GetAllHotelsResponse>> GetAllHotelsAsync(CancellationToken ct = default)
        {
            return await dbContext.Hotels
                .AsNoTracking()
                .ProjectToType<GetAllHotelsResponse>()
                .ToListAsync(ct);
        }

        public async Task<GetHotelByIdResponse?> GetHotelByIdAsync(int Id, CancellationToken ct = default)
        {
            return await dbContext.Hotels
                .AsNoTracking()
                .ProjectToType<GetHotelByIdResponse>()
                .FirstOrDefaultAsync(h => h.Id == Id, ct);
        }
    }
}

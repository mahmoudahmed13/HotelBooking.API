using HotelBooking.Domain.Common;
using HotelBooking.UseCases.Hotels.DTOs;

namespace HotelBooking.UseCases.Hotels.Queries
{
    public class GetAllHotelQuery(IHotelQueryService hotelQueryService)
    {
        public async Task<Result<IReadOnlyList<GetAllHotelsResponse>>> ExecuteAsync(CancellationToken ct)
        {
            var hotels = await hotelQueryService.GetAllHotelsAsync(ct);
            return Result<IReadOnlyList<GetAllHotelsResponse>>.Ok(hotels);
        }
    }
}

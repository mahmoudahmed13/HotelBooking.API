using HotelBooking.Domain.Common;
using HotelBooking.UseCases.Hotels.DTOs;

namespace HotelBooking.UseCases.Hotels.Queries
{
    public class GetHotelByIdQuery(IHotelQueryService hotelQueryService)
    {
        public async Task<Result<GetHotelByIdResponse>> ExecuteAsync(int id)
        {
            var hotel = await hotelQueryService.GetHotelByIdAsync(id);
            if (hotel == null)
                Error.Failure("Hotel Not Found.", $"Hotel with id {id} is not Found");
            return Result<GetHotelByIdResponse>.Ok(hotel);
        }
    }
}

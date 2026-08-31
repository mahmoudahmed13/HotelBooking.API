using HotelBooking.UseCases.Hotels.DTOs;
using HotelBooking.UseCases.Hotels.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers.V1
{
    public class HotelsController(GetAllHotelQuery getAllHotelQuery) : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetAllHotelsResponse>>> GetAllHotels(CancellationToken ct)
        {
            var hotels = await getAllHotelQuery.ExecuteAsync(ct);
            return Ok(hotels.Value);
        }
    }
}

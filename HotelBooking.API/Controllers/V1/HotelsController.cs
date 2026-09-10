using HotelBooking.Domain.Common;
using HotelBooking.UseCases.Hotels.DTOs;
using HotelBooking.UseCases.Hotels.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers.V1
{
    public class HotelsController(IMediator mediator) : ApiControllerBase
    {
        //http://localhost:5186/api/v1/Hotels/1
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetHotelByIdResponse>> GetById(int id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetHotelByIdQuery(id), ct);
            return result.Match<ActionResult<GetHotelByIdResponse>>(
                 value => Ok(value), error => NotFound(error));
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetAllHotelsResponse>>> GetAllHotels(CancellationToken ct)
        {
            var hotels = await mediator.Send(new GetAllHotelQuery(), ct);
            return ToActionResult(hotels);
        }
    }
}

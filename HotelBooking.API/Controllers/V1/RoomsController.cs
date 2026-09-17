using HotelBooking.UseCases.Rooms.DTOs;
using HotelBooking.UseCases.Rooms.Queries;
using HotelBooking.UseCases.Rooms.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers.V1
{

    public class RoomsController(IMediator mediator) : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<GetAllRoomsResponse>>> GetAllRooms([FromQuery] RoomQueryParams queryParams,CancellationToken ct)
        {
            var result = await mediator.Send(new GetAllRoomsQuery(queryParams), ct);
            return ToActionResult(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetRoomByIdResponse>> GetById(int id, CancellationToken ct)
            => ToActionResult(await mediator.Send(new GetRoomByIdQuery(id), ct));
    }
}

using HotelBooking.UseCases.ReservationRooms.DTOs;
using HotelBooking.UseCases.ReservationRooms.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers.V1
{

    public class ReservationRoomsController(IMediator mediator) : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetAllReservationRoomsResponse>>> GetAll(
            int? reservationId, int? roomId, string? searchValue, CancellationToken cancellationToken)
         => ToActionResult(await mediator.Send(new GetAllReservationRoomsQuery(reservationId,roomId, searchValue), cancellationToken));

    }
}

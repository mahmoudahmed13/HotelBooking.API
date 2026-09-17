using HotelBooking.UseCases.Reservations.DTOs;
using HotelBooking.UseCases.Reservations.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelBooking.API.Controllers.V1
{
    
    public class ReservationsController(IMediator mediator) : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetAllReservationsResponse>>> GetAll(CancellationToken ct)
            => ToActionResult(await mediator.Send(new GetAllReservationsQuery(), ct));
        
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetReservationByIdResponse>> GetById(int Id, CancellationToken ct)
            => ToActionResult(await mediator.Send(new GetReservationByIdQuery(Id), ct));
    }
}

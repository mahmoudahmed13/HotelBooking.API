using HotelBooking.Domain.Common;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Repositories;
using HotelBooking.UseCases.Reservations.DTOs;
using Mapster;
using MediatR;

namespace HotelBooking.UseCases.Reservations.Queries.Handlers
{
    public class GetReservationByIdQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetReservationByIdQuery, Result<GetReservationByIdResponse>>
    {
        public async Task<Result<GetReservationByIdResponse>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await unitOfWork.GetRepository<Reservation, int>()
                .GetByIdAsync(request.Id, cancellationToken);
            if (reservation is null)
                return Error.NotFound("Reservation not found");
            return reservation.Adapt<GetReservationByIdResponse>();
        }
    }
}

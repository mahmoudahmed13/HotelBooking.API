using HotelBooking.Domain.Common;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Repositories;
using HotelBooking.UseCases.Reservations.DTOs;
using Mapster;
using MediatR;

namespace HotelBooking.UseCases.Reservations.Queries.Handlers
{
    public class GetAllReservationsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetAllReservationsQuery, Result<IReadOnlyList<GetAllReservationsResponse>>>
    {
        public async Task<Result<IReadOnlyList<GetAllReservationsResponse>>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await unitOfWork.GetRepository<Reservation, int>()
                .GetAllAsync(cancellationToken);
            var reservationsResponse = reservations.Adapt<IReadOnlyList<GetAllReservationsResponse>>();
            return Result<IReadOnlyList<GetAllReservationsResponse>>.Ok(reservationsResponse);
        }
    }
}

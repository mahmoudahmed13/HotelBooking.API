using HotelBooking.Domain.Common;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Repositories;
using HotelBooking.UseCases.ReservationRooms.DTOs;
using HotelBooking.UseCases.ReservationRooms.Specifications;
using Mapster;
using MediatR;

namespace HotelBooking.UseCases.ReservationRooms.Queries.Handlers
{
    public class GetAllReservationRoomsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetAllReservationRoomsQuery, Result<IReadOnlyList<GetAllReservationRoomsResponse>>>
    {
        public async Task<Result<IReadOnlyList<GetAllReservationRoomsResponse>>> Handle(
            GetAllReservationRoomsQuery request, CancellationToken cancellationToken)
        {
            var spec = new ReservationRoomsSpecification(request.reservationId, request.roomId, request.searchValue);
            var reservationRooms = await unitOfWork.GetRepository<ReservationRoom, int>()
                .GetAllAsync(spec,cancellationToken);
            var reservationRoomsResponse = reservationRooms.Adapt<IReadOnlyList<GetAllReservationRoomsResponse>>();
            return Result<IReadOnlyList<GetAllReservationRoomsResponse>>.Ok(reservationRoomsResponse);
        }
    }
}

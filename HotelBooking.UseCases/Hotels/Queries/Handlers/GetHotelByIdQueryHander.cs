using HotelBooking.Domain.Common;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Repositories;
using HotelBooking.UseCases.Hotels.DTOs;
using Mapster;
using MediatR;

namespace HotelBooking.UseCases.Hotels.Queries.Handlers
{
    public class GetHotelByIdQueryHander(IUnitOfWork unitOfWork)
        : IRequestHandler<GetHotelByIdQuery, Result<GetHotelByIdResponse>>
    {
        public async Task<Result<GetHotelByIdResponse>> Handle(
            GetHotelByIdQuery request, CancellationToken ct)
        {
            var hotel = await unitOfWork.GetRepository<Hotel, int>()
                .GetByIdAsync(request.Id, ct);
            if (hotel is null)
                return Error.NotFound("Hotel Not Found.", $"Hotel with id {request.Id} is not Found");
            return Result<GetHotelByIdResponse>.Ok(hotel.Adapt<GetHotelByIdResponse>());
        }
    }
}

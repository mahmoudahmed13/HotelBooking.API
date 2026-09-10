using HotelBooking.Domain.Common;
using HotelBooking.UseCases.Hotels.DTOs;
using MediatR;

namespace HotelBooking.UseCases.Hotels.Queries
{
    public class GetAllHotelQuery() : IRequest<Result<IReadOnlyList<GetAllHotelsResponse>>>;
    
}

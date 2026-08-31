using HotelBooking.Domain.Entities;
using HotelBooking.UseCases.Reservations.DTOs;
using Mapster;

namespace HotelBooking.UseCases.Profiles
{
    public class ReservationMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Reservation, GetAllReservationsResponse>()
                .Map(dest => dest.Status, src => src.Status.ToString());

            config.NewConfig<Reservation, GetReservationByIdResponse>()
                .Map(dest => dest.Status, src => src.Status.ToString());
        }
    }
}

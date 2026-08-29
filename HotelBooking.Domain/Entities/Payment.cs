using HotelBooking.Domain.Entities.Enums;

namespace HotelBooking.Domain.Entities
{
    public class Payment : BaseEntity<int>
    {
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public DateTimeOffset Date { get; set; }
        public string ConfirmationNumber { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;
    }
}

using HotelBooking.Domain.Entities.Enums;

namespace HotelBooking.Domain.Entities
{
    public class Reservation : BaseEntity<int>
    {
        public const int MaxNameLengh = 100;
        public DateOnly BookingDate { get; private set; }
        public DateOnly CheckInDate { get; private set; }
        public DateOnly CheckOutDate { get; private set; }
        public ReservationStatus Status { get; private set; } = ReservationStatus.Pending;
        public decimal TotalPrice { get; private set; }
        public int NumberOfAdults { get; private set; }
        public int NumberOfChildren { get; private set; }
        public string GuestFullName { get; private set; } = default!;
        public string GuestEmail { get; private set; } = default!;
        public string GuestPhone { get; private set; } = default!;

        public ICollection<ReservationRoom> ReservationRooms { get; private set; } = [];
        public ICollection<Payment> Payments { get; private set; } = [];

        // EF Core needs a parameterless constructor to materialize entities from the
        // database. It can be private - EF Core can still use it via reflection,
        // and no application code outside this class can call it.
        private Reservation()
        {
        }

        private Reservation(
            DateOnly checkInDate,
            DateOnly checkOutDate,
            decimal totalPrice,
            int numberOfAdults,
            int numberOfChildren,
            string guestFullName,
            string guestEmail,
            string guestPhone)
        {
            SetCheckInAndOutDate(checkInDate, checkOutDate);
            SetTotalPrice(totalPrice);
            SetNumberOfAdults(numberOfAdults);
            SetNumberOfChildren(numberOfChildren);
            SetGuestFullName(guestFullName);
            SetGuestEmail(guestEmail);
            GuestPhone = guestPhone;
            BookingDate = DateOnly.FromDateTime(DateTime.UtcNow);
            Status = ReservationStatus.Pending;
        }
        public static Reservation Create(DateOnly checkInDate,
            DateOnly checkOutDate,
            decimal totalPrice,
            int numberOfAdults,
            int numberOfChildren,
            string guestFullName,
            string guestEmail,
            string guestPhone)
        {
            return new Reservation(
                checkInDate,
                checkOutDate,
                totalPrice,
                numberOfAdults,
                numberOfChildren,
                guestFullName,
                guestEmail,
                guestPhone);
        }
        private void SetCheckInAndOutDate(DateOnly checkInDate, DateOnly checkOutDate)
        {
            if (checkOutDate <= checkInDate)
                throw new ArgumentException("Check-out date must be after check-in date.");
            if (checkInDate >= DateOnly.FromDateTime(DateTime.UtcNow))
                throw new ArgumentException("Check In date must be in the present or future");
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }
        private void SetNumberOfChildren(int numberOfChildren)
        {
            if (numberOfChildren < 0)
                throw new ArgumentException("Number of children cannot be negative.");
            NumberOfChildren = numberOfChildren;

        }
        private void SetNumberOfAdults(int numberOfAdults)
        {

            if (numberOfAdults < 1)
                throw new ArgumentException("A reservation must have at least one adult.");
            NumberOfAdults = numberOfAdults;
        }
        private void SetGuestFullName(string guestFullName)
        {
            if (string.IsNullOrWhiteSpace(guestFullName))
                throw new ArgumentException("Guest full name is required.");
            if (guestFullName.Length > MaxNameLengh)
                throw new ArgumentException($"Guest full name can not exeed {MaxNameLengh} characters.");
            GuestFullName = guestFullName;
        }
        private void SetGuestEmail(string guestEmail)
        {
            if (string.IsNullOrWhiteSpace(guestEmail) || !guestEmail.Contains('@'))
                throw new ArgumentException("A valid guest email is required.");
            GuestEmail = guestEmail;
        }
        private void SetTotalPrice(decimal totalPrice)
        {
            if (totalPrice < 0)
                throw new ArgumentException("Total price cannot be negative.");

            TotalPrice = totalPrice;
        }

        public void Confirm()
        {
            if (Status != ReservationStatus.Pending)
                throw new InvalidOperationException($"Cannot confirm a reservation with status '{Status}'.");

            Status = ReservationStatus.Confirmed;
        }

        public void Cancel()
        {
            if (Status == ReservationStatus.CheckedOut)
                throw new InvalidOperationException("Cannot cancel a reservation that has already been checked out.");

            Status = ReservationStatus.Cancelled;
        }
    }
}
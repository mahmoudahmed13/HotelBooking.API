using HotelBooking.Domain.Common;
using HotelBooking.Domain.Entities.Enums;

namespace HotelBooking.Domain.Entities
{
    public class Reservation : BaseEntity<int>
    {
        public const int MaxNameLength = 100;

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
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            TotalPrice = totalPrice;
            NumberOfAdults = numberOfAdults;
            NumberOfChildren = numberOfChildren;
            GuestFullName = guestFullName;
            GuestEmail = guestEmail;
            GuestPhone = guestPhone;
            BookingDate = DateOnly.FromDateTime(DateTime.UtcNow);
            Status = ReservationStatus.Pending;
        }

        // The only public entry point for creating a Reservation.
        // Validates everything up front, collects ALL failures (not just the
        // first one) via Result.Combine, and only constructs the entity once
        // every rule has passed.
        public static Result<Reservation> Create(
            DateOnly checkInDate,
            DateOnly checkOutDate,
            decimal totalPrice,
            int numberOfAdults,
            int numberOfChildren,
            string guestFullName,
            string guestEmail,
            string guestPhone)
        {
            var validation = Result.Combine(
                ValidateCheckInAndOutDate(checkInDate, checkOutDate),
                ValidateTotalPrice(totalPrice),
                ValidateNumberOfAdults(numberOfAdults),
                ValidateNumberOfChildren(numberOfChildren),
                ValidateGuestFullName(guestFullName),
                ValidateGuestEmail(guestEmail),
                ValidateGuestPhone(guestPhone));

            if (validation.IsFailure)
                return Result<Reservation>.Fail(validation.Errors);

            var reservation = new Reservation(
                checkInDate,
                checkOutDate,
                totalPrice,
                numberOfAdults,
                numberOfChildren,
                guestFullName,
                guestEmail,
                guestPhone);

            return reservation;
        }

        private static Result ValidateCheckInAndOutDate(DateOnly checkInDate, DateOnly checkOutDate)
        {
            if (checkOutDate <= checkInDate)
                return Error.Failure("Check-out date must be after check-in date.");

            if (checkInDate < DateOnly.FromDateTime(DateTime.UtcNow))
                return Error.Failure("Check-in date must be in the present or future.");

            return Result.Ok();
        }

        private static Result ValidateTotalPrice(decimal totalPrice)
        {
            if (totalPrice < 0)
                return Error.Failure("Total price cannot be negative.");

            return Result.Ok();
        }

        private static Result ValidateNumberOfAdults(int numberOfAdults)
        {
            if (numberOfAdults < 1)
                return Error.Failure("A reservation must have at least one adult.");

            return Result.Ok();
        }

        private static Result ValidateNumberOfChildren(int numberOfChildren)
        {
            if (numberOfChildren < 0)
                return Error.Failure("Number of children cannot be negative.");

            return Result.Ok();
        }

        private static Result ValidateGuestFullName(string guestFullName)
        {
            if (string.IsNullOrWhiteSpace(guestFullName))
                return Error.Failure("Guest full name is required.");

            if (guestFullName.Length > MaxNameLength)
                return Error.Failure($"Guest full name cannot exceed {MaxNameLength} characters.");

            return Result.Ok();
        }

        private static Result ValidateGuestEmail(string guestEmail)
        {
            if (string.IsNullOrWhiteSpace(guestEmail) || !guestEmail.Contains('@'))
                return Error.Failure("A valid guest email is required.");

            return Result.Ok();
        }

        private static Result ValidateGuestPhone(string guestPhone)
        {
            // Wasn't validated at all before - added for consistency with
            // the other guest fields. Remove if phone is meant to be optional.
            if (string.IsNullOrWhiteSpace(guestPhone))
                return Error.Failure("Guest phone is required.");

            return Result.Ok();
        }

        public Result Confirm()
        {
            if (Status != ReservationStatus.Pending)
                return Error.Failure($"Cannot confirm a reservation with status '{Status}'.");

            Status = ReservationStatus.Confirmed;
            return Result.Ok();
        }

        public Result Cancel()
        {
            if (Status == ReservationStatus.CheckedOut)
                return Error.Failure("Cannot cancel a reservation that has already been checked out.");

            Status = ReservationStatus.Cancelled;
            return Result.Ok();
        }
    }
}
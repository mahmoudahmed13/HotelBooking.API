namespace HotelBooking.Domain.Common
{

    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public IReadOnlyList<Error> Errors { get; }

        protected Result(bool isSuccess, IReadOnlyList<Error> errors)
        {
            if (isSuccess && errors.Count > 0)
                throw new InvalidOperationException("A successful result cannot contain errors.");

            if (!isSuccess && errors.Count == 0)
                throw new InvalidOperationException("A failed result must contain at least one error.");

            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Ok() => new(true, Array.Empty<Error>());
        public static Result Fail(Error error) => new(false, new[] { error });
        public static Result Fail(IReadOnlyList<Error> errors) => new(false, errors);

        public static implicit operator Result(Error error) => Fail(error);

        // Runs several independent checks and merges every failure into one
        // Result instead of stopping at the first one - useful when validating
        // several fields/rules at once and wanting to report all problems together.
        public static Result Combine(params Result[] results)
        {
            var errors = results
                .Where(r => r.IsFailure)
                .SelectMany(r => r.Errors)
                .ToList();

            return errors.Count == 0 ? Ok() : Fail(errors);
        }

        // Functional-style branching: turns a Result into whatever type the
        // caller needs (an IActionResult, a DTO, a string...) in one expression,
        // instead of an if/else checking IsSuccess everywhere it's consumed.
        public TOut Match<TOut>(Func<TOut> onSuccess, Func<IReadOnlyList<Error>, TOut> onFailure) =>
            IsSuccess ? onSuccess() : onFailure(Errors);
        
        //logger.LogWarning(result.ToString())
        public override string ToString()
        {
            return IsSuccess
                ? "Success"
                : $"Failure: {string.Join("; ", Errors.Select(e => e.Message))}";
        }
    }

    // The "content" version: same success/failure shape as Result, but a
    // successful outcome also carries a value (e.g. the Reservation that was
    // just created).
    public class Result<T> : Result
    {
        private readonly T? _value;
        public T Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Cannot access Value on a failed result.");

        private Result(T? value, bool isSuccess, IReadOnlyList<Error> errors)
            : base(isSuccess, errors)
        {   
            _value = value;
        }

        public static Result<T> Ok(T value) => new(value, true, Array.Empty<Error>());
        public new static Result<T> Fail(Error error) => new(default, false, new[] { error });
        public new static Result<T> Fail(IReadOnlyList<Error> errors) => new(default, false, errors);

        // return someReservation;            instead of  return Result<Reservation>.Ok(someReservation);
        // return someError;                  instead of  return Result<Reservation>.Fail(someError);
        public static implicit operator Result<T>(T value) => Ok(value);
        public static implicit operator Result<T>(Error error) => Fail(error);

        public TOut Match<TOut>(Func<T, TOut> onSuccess, Func<IReadOnlyList<Error>, TOut> onFailure) =>
            IsSuccess ? onSuccess(Value) : onFailure(Errors);
        public override string ToString()
        {
            return IsSuccess
                ? $"Success: {Value}"
                : $"Failure: {string.Join("; ", Errors.Select(e => e.Message))}";
        }
    }
}

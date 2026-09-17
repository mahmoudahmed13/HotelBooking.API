using HotelBooking.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class ApiControllerBase : ControllerBase
    {
        public static ActionResult<T> ToActionResult<T>(Result<T> result) =>
            result.Match<ActionResult>(
                onSuccess: value => new OkObjectResult(value),
                onFailure: errors => ToProblem(errors));

        public static ActionResult ToActionResult(Result result) =>
            result.Match<ActionResult>(
                onSuccess: () => new OkResult(),
                onFailure: errors => ToProblem(errors));

        protected static ObjectResult ToProblem(IReadOnlyList<Error> errors)
        {
            var statusCode = errors[0].Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.UnAuthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.InvalidCredentials => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = errors[0].Code,
                Detail = errors[0].Message,
                Extensions = { ["errors"] = errors }
            };

            return new ObjectResult(problem) { StatusCode = statusCode};
        }
    }
}

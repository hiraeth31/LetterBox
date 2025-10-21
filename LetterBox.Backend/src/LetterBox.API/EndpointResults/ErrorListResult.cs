using LetterBox.API.Response;
using LetterBox.Domain.Common;

namespace LetterBox.API.EndpointResults
{
    public sealed class ErrorListResult : IResult
    {
        private readonly ErrorList _errorList;

        public ErrorListResult(Error error)
        {
            _errorList = error.ToErrorList();
        }

        public ErrorListResult(ErrorList errorList)
        {
            _errorList = errorList;
        }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext);

            if (!_errorList.Any())
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                return httpContext.Response.WriteAsJsonAsync(Envelope.Error(_errorList));
            }

            var distinctErrorTypes = _errorList
                .Select(x => x.Type)
                .Distinct()
                .ToList();

            int statusCode = distinctErrorTypes.Count > 1
                ? StatusCodes.Status500InternalServerError
                : GetStatusCodeForErrorType(distinctErrorTypes.First());

            var envelope = Envelope.Error(_errorList);
            httpContext.Response.StatusCode = statusCode;

            return httpContext.Response.WriteAsJsonAsync(envelope);
        }

        private static int GetStatusCodeForErrorType(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError,
            };
    }
}

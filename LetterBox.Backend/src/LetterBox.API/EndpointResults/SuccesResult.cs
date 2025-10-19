using LetterBox.API.Response;
using System.Net;

namespace LetterBox.API.EndpointResults
{
    public sealed class SuccesResult<TValue> : IResult
    {
        private readonly TValue _value;

        public SuccesResult(TValue value)
        {
            _value = value;
        }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext);

            var envelope = Envelope.Ok(_value);

            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;

            return httpContext.Response.WriteAsJsonAsync(envelope);
        }
    }
}

using CSharpFunctionalExtensions;
using LetterBox.API.Response;
using LetterBox.Domain.Common;
using Microsoft.AspNetCore.Http.Metadata;
using System.Reflection;

namespace LetterBox.API.EndpointResults
{
    public sealed class EndpointResult<TValue> : Microsoft.AspNetCore.Http.IResult, IEndpointMetadataProvider
    {
        private readonly Microsoft.AspNetCore.Http.IResult _result;

        public EndpointResult(Result<TValue, Error> result)
        {
            _result = result.IsSuccess
                ? new SuccesResult<TValue>(result.Value)
                : new ErrorListResult(result.Error);
        }

        public EndpointResult(Result<TValue, ErrorList> result)
        {
            _result = result.IsSuccess
                ? new SuccesResult<TValue>(result.Value)
                : new ErrorListResult(result.Error);
        }

        public Task ExecuteAsync(HttpContext httpContext) =>
            _result.ExecuteAsync(httpContext);

        public static implicit operator EndpointResult<TValue>(Result<TValue, Error> result) => new(result);

        public static implicit operator EndpointResult<TValue>(Result<TValue, ErrorList> result) => new(result);

        public static void PopulateMetadata(MethodInfo method, EndpointBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(method);
            ArgumentNullException.ThrowIfNull(builder);

            builder.Metadata.Add(new ProducesResponseTypeMetadata(200, typeof(Envelope<TValue>), ["application/json"]));

            builder.Metadata.Add(new ProducesResponseTypeMetadata(500, typeof(Envelope<TValue>), ["application/json"]));
            builder.Metadata.Add(new ProducesResponseTypeMetadata(400, typeof(Envelope<TValue>), ["application/json"]));
            builder.Metadata.Add(new ProducesResponseTypeMetadata(404, typeof(Envelope<TValue>), ["application/json"]));
            builder.Metadata.Add(new ProducesResponseTypeMetadata(401, typeof(Envelope<TValue>), ["application/json"]));
            builder.Metadata.Add(new ProducesResponseTypeMetadata(403, typeof(Envelope<TValue>), ["application/json"]));
            builder.Metadata.Add(new ProducesResponseTypeMetadata(409, typeof(Envelope<TValue>), ["application/json"]));
        }
    }
}

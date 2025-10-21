using LetterBox.Domain.Common;
using System.Text.Json.Serialization;

namespace LetterBox.API.Response
{
    public record Envelope
    {
        public object? Result { get; }
        public ErrorList? ErrorList { get; }
        public bool isError => ErrorList != null || ErrorList != null && ErrorList.Any();
        public DateTime TimeGenerated { get; }

        [JsonConstructor]
        public Envelope(object? result, ErrorList? errorList)
        {
            Result = result;
            ErrorList = errorList;
            TimeGenerated = DateTime.Now;
        }

        public static Envelope Ok(object? result = null) =>
            new(result, null);

        public static Envelope Error(ErrorList errors) =>
            new(null, errors);
    }

    public record Envelope<T>
    {
        public T? Result { get; }
        public ErrorList? ErrorList { get; }
        public bool isError => ErrorList != null || ErrorList != null && ErrorList.Any();
        public DateTime TimeGenerated { get; }

        [JsonConstructor]
        public Envelope(T? result, ErrorList? errorList)
        {
            Result = result;
            ErrorList = errorList;
            TimeGenerated = DateTime.Now;
        }

        public static Envelope Ok(T? result = default) =>
            new(result, null);
        public static Envelope Error(ErrorList errors) =>
            new(default, errors);
    }
}

namespace LetterBox.Domain.Common
{
    public class Error
    {
        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }
        public string? InvalidField { get; } = null;

        private Error(string code, string message, ErrorType type, string? invalidField = null)
        {
            Code = code;
            Message = message;
            Type = type;
            InvalidField = invalidField;
        }

        public static Error Validation(string code, string message, string? invalidField = null)
            => new(code, message, ErrorType.Validation, invalidField);

        public static Error NotFound(string code, string message)
            => new(code, message, ErrorType.NotFound);

        public static Error Failure(string code, string message)
            => new(code, message, ErrorType.Failure);

        public static Error Conflict(string code, string message)
            => new(code, message, ErrorType.Conflict);

        public ErrorList ToErrorList() => new([this]);
    }

    public enum ErrorType
    {
        Validation,
        NotFound,
        Failure,
        Conflict
    }
}

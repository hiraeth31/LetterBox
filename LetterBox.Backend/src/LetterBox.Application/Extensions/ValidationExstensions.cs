using LetterBox.Domain.Common;
using CSharpFunctionalExtensions;
using FluentValidation.Results;

namespace LetterBox.Application.Extensions
{
    public static class ValidationExstensions
    {
        public static ErrorList ToErrorList(this ValidationResult validationResult)
        {
            var validationErrors = validationResult.Errors;

            var errors = from validationError in validationErrors
                         let errorMessage = validationError.ErrorMessage
                         let error = Error.Deserialize(errorMessage)
                         select Error.Validation(error.Code, errorMessage, validationError.PropertyName);

            return errors.ToList();
        }
    }
}

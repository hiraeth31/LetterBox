using FluentValidation;
using LetterBox.Application.Validation;
using LetterBox.Domain.Common;

namespace LetterBox.Application.Categories.AddCategory
{
    public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
    {

        public AddCategoryValidator()
        {
            RuleFor(s => s.Excerpt).MinimumLength(2)
                                   .WithError(Errors.General.ValueIsInvalid("min 2 characters"));

            RuleFor(s => s.Name).MinimumLength(5)
                                   .WithError(Errors.General.ValueIsInvalid("min 5 characters length"));
        }
    }
}

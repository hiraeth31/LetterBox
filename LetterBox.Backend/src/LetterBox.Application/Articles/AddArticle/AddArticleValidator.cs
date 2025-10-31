using FluentValidation;
using LetterBox.Application.Validation;
using LetterBox.Domain.Common;

namespace LetterBox.Application.Articles.AddArticle
{
    public class AddArticleValidator : AbstractValidator<AddArticleCommand>
    {

        public AddArticleValidator()
        {
            RuleFor(s => s.Title).MinimumLength(3)
                                 .WithError(Errors.General.ValueIsInvalid("min 3 characters")); ;
        }
    }
}

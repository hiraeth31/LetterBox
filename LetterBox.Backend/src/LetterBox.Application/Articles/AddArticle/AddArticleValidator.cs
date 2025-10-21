using FluentValidation;

namespace LetterBox.Application.Articles.AddArticle
{
    public class AddArticleValidator : AbstractValidator<AddArticleCommand>
    {

        public AddArticleValidator()
        {
            RuleFor(s => s.Title).MinimumLength(3);


        }
    }
}

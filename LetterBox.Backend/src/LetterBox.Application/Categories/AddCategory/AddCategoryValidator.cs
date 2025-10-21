using FluentValidation;

namespace LetterBox.Application.Categories.AddCategory
{
    public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
    {

        public AddCategoryValidator()
        {
            RuleFor(s => s.Excerpt).MinimumLength(2);
        }
    }
}

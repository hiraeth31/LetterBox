using FluentValidation;

namespace LetterBox.Application.Categories.AddCategory
{
    public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
    {

        public AddCategoryValidator()
        {
            //RuleFor(s => s.Excerpt).NotEmpty().When(s => s.Excerpt != null);
            RuleFor(s => s.Description).MinimumLength(5);
            RuleFor(s => s.Excerpt).NotEmpty().NotNull();
        }
    }
}

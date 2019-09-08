using BunBlog.API.Models.Categories;
using FluentValidation;


namespace BunBlog.API.Validators.Categories
{
    public class CategoryModelValidator : AbstractValidator<CategoryModel>
    {
        public CategoryModelValidator()
        {
            RuleFor(m => m.LinkName)
                .NotEmpty()
                .MaximumLength(100)
                .LinkName();

            RuleFor(m => m.DisplayName)
                .NotEmpty();
        }
    }
}

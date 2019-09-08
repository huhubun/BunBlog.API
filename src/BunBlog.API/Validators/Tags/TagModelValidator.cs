using BunBlog.API.Models.Tags;
using FluentValidation;

namespace BunBlog.API.Validators.Tags
{
    public class TagModelValidator : AbstractValidator<TagModel>
    {
        public TagModelValidator()
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

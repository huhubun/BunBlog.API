using BunBlog.API.Models.Posts;
using FluentValidation;

namespace BunBlog.API.Validators.Posts
{
    public class CreateBlogPostModelValidator : AbstractValidator<CreateBlogPostModel>
    {
        public CreateBlogPostModelValidator()
        {
            RuleFor(m => m.LinkName)
                .NotEmpty()
                .MaximumLength(100)
                .LinkName();
        }
    }
}

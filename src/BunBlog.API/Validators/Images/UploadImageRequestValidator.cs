using BunBlog.API.Models.Images;
using FluentValidation;

namespace BunBlog.API.Validators.Images
{
    public class UploadImageRequestValidator : AbstractValidator<UploadImageRequest>
    {
        public UploadImageRequestValidator()
        {
            RuleFor(m => m.Image)
                .Must(image =>
                {
                    return image != null && image.Length > 0;
                }).WithMessage("必须上传一个图片");
        }
    }
}

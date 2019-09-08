using BunBlog.API.Const;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BunBlog.API.Models
{
    public class BunBlogValidationProblemDetails : ValidationProblemDetails
    {
        public BunBlogValidationProblemDetails(ModelStateDictionary modelState) : base(modelState)
        {
        }

        public string Code { get; } = ErrorResponseCode.MODEL_VALIDATION_ERROR;

        public string TraceId { get; set; }
    }
}

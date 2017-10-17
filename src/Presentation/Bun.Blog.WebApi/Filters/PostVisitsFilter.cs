using Bun.Blog.Services.Posts;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.WebApi.Filters
{
    public class PostVisitsFilter : IActionFilter
    {
        private readonly IPostMetaService _postMetaService;

        public PostVisitsFilter(IPostMetaService postMetaService)
        {
            _postMetaService = postMetaService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var postId = Convert.ToInt32(context.RouteData.Values["id"]);

            if (postId != default(int))
            {
                _postMetaService.AddVisits(postId);
            }
        }
    }
}

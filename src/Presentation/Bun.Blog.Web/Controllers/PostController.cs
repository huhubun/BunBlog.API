using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.Core.Domain.Users;
using Bun.Blog.Data.Extensions;
using Bun.Blog.Services.Posts;
using Bun.Blog.Web.Extensions;
using Bun.Blog.Web.Framework.Mvc.Controllers;
using Bun.Blog.Web.Framework.Mvc.Json;
using Bun.Blog.Web.Models.Posts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Bun.Blog.Web.Controllers
{

    public class PostController : AdminBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IPostService _postService;

        public PostController(UserManager<User> userManager,
            IPostService postService)
        {
            _userManager = userManager;
            _postService = postService;
        }

        public IActionResult List()
        {
            var model = new PostListModel
            {
                PostList = _postService.GetAll().MapTo<IList<Post>, List<PostModel>>()
            };

            return View(model);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(PostNewModel model)
        {
            var post = model.MapToEntity();
            post.AuthorId = _userManager.GetUserId(User);

            _postService.Add(post);

            return BunJson(new { post.Id });
        }
    }
}

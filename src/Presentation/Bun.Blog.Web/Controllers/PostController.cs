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
using System.Text.Encodings.Web;
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
            if (ModelState.IsValid)
            {
                var post = model.MapToEntity();
                post.AuthorId = _userManager.GetUserId(User);

                _postService.Add(post);

                SuccessNotification("文章发布成功");

                return RedirectToAction(nameof(Edit), new { post.Id });
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var post = _postService.GetById(id);

            if (post != null)
            {
                var model = post.MapTo<Post, EditPostModel>();

                return View(model);
            }
            else
            {
                return Content($"未找到 id 为 {id} 的文章");
            }

        }
    }
}

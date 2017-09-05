using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.Core.Domain.Users;
using Bun.Blog.Services.Posts;
using Bun.Blog.Web.Admin.Extensions;
using Bun.Blog.Web.Admin.Models.Posts;
using Bun.Blog.Web.Framework.Mvc.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Bun.Blog.Web.Admin.Controllers
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
                var post = model.ToEntity();
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

        [HttpPost]
        public IActionResult Edit(EditPostModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _postService.GetById(model.Id);
                entity = model.ToEntity(entity);
                _postService.Update(entity);

                SuccessNotification("更新文章成功");

                return RedirectToAction(nameof(Edit), new { model.Id });
            }

            return View(model);
        }
    }
}

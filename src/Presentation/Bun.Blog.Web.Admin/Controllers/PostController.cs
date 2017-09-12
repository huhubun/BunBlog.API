using AutoMapper;
using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.Core.Domain.Users;
using Bun.Blog.Services.Posts;
using Bun.Blog.Web.Admin.Models.Posts;
using Bun.Blog.Web.Framework.Mvc.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
                PostList = Mapper.Map<List<Post>, List<PostModel>>(_postService.GetAll())
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View("Edit", new PostModel());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = _postService.GetById(id);

            if (post != null)
            {
                return View(Mapper.Map<Post, PostModel>(post));
            }
            else
            {
                return Content($"未找到 id 为 {id} 的文章");
            }
        }

        [HttpPost]
        public IActionResult Save(PostModel model)
        {
            model.AuthorId = _userManager.GetUserId(User);
            Post entity;

            if (model.IsNew)
            {
                if (String.IsNullOrEmpty(model.Title))
                {
                    model.Title = DateTime.Now.ToLongDateString();
                }

                if (String.IsNullOrEmpty(model.Excerpt))
                {
                    model.Excerpt = new String((model.Content ?? String.Empty).Take(50).ToArray());
                }

                entity = Mapper.Map<PostModel, Post>(model);
                entity = _postService.Add(entity);
            }
            else
            {
                entity = _postService.GetById(model.Id);
                entity = Mapper.Map(model, entity);
                entity = _postService.Update(entity);
            }

            return Json(new SavePostResult
            {
                Id = entity.Id,
                Title = entity.Title
            });
        }



        //[HttpPost]
        //public IActionResult Edit(EditPostModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var entity = _postService.GetById(model.Id);
        //        entity = model.ToEntity(entity);
        //        _postService.Update(entity);

        //        SuccessNotification("更新文章成功");

        //        return RedirectToAction(nameof(Edit), new { model.Id });
        //    }

        //    return View(model);
        //}
    }
}

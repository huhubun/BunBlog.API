using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BunBlog.API.Models.Categories;
using BunBlog.API.Models.Posts;
using BunBlog.API.Models.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        public PostsController()
        {

        }

        public IActionResult GetList()
        {
            return Ok(new List<BlogPostModel>
            {
                new BlogPostModel
                {
                    Title = "Test blog",
                    Visits = 2,
                    PublishedOn = new DateTime(2019, 7, 26, 12, 13, 14),
                    Tags = new List<TagModel>{ new TagModel {  LinkName = "a-tag", Name = "AAA"}, new TagModel { LinkName = "test", Name = "test" } },
                    Category = new CategoryModel{ LinkName="category", Name = "My Category" }
                }
            });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(new { post_id = id, date = DateTime.Now });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(CreateBlogPostModel model)
        {
            //return Created($"~/{model.Title}", model);
            return Ok();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bun.Blog.Services.Posts;
using AutoMapper;
using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.WebApi.Models.Posts;
using Bun.Blog.WebApi.Filters;

namespace Bun.Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly IPostMetaService _postMetaService;

        public PostsController(IPostService postService,
            IPostMetaService postMetaService)
        {
            _postService = postService;
            _postMetaService = postMetaService;
        }

        [HttpGet, Route("")]
        public IActionResult GetAll()
        {
            var posts = _postService.GetAll();

            return Ok(Mapper.Map<List<Post>, IEnumerable<PostListItem>>(posts));
        }

        [HttpGet, Route("{postId:int}", Name = nameof(GetById))]
        [ServiceFilter(typeof(PostVisitsFilter))]
        public IActionResult GetById([FromRoute] int postId)
        {
            var post = _postService.GetById(postId);

            return Ok(Mapper.Map<Post, PostDetailModel>(post));
        }

        [HttpGet, Route("{postId:int}/metas")]
        public IActionResult GetMetas([FromRoute] int postId)
        {
            if (!_postService.Exists(postId))
            {
                return NotFound();
            }

            var metas = _postMetaService.GetList(postId);

            return Ok(metas.ToDictionary(m => m.MetaKey, m => m.MetaValue));
        }

        [HttpGet, Route("{postId:int}/metas/{metaKey}")]
        public IActionResult GetMeta([FromRoute] int postId, [FromRoute] string metaKey)
        {
            if (!PostMetaKey.IsKey(metaKey))
            {
                return BadRequest();
            }

            if (!_postService.Exists(postId))
            {
                return NotFound();
            }

            var postMeta = _postMetaService.GetMeta(postId, metaKey);
            if (postMeta == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PostMeta, PostMetaModel>(postMeta));
        }
    }
}

using Bun.Blog.Core.Data;
using Bun.Blog.Core.Domain.Posts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Bun.Blog.Data;
using System.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;

namespace Bun.Blog.Services.Posts
{
    public class PostMetaService : IPostMetaService
    {
        private readonly DbContext _context;
        private IRepository<PostMeta> _postMetaRepository;
        private IPostService _postService;

        public PostMetaService(BlogContext context,
            IRepository<PostMeta> postMetaRepository,
            IPostService postService)
        {
            _context = context;
            _postMetaRepository = postMetaRepository;
            _postService = postService;
        }

        public void AddVisits(int postId)
        {
            if (_postService.Exists(postId))
            {
                if (!_postMetaRepository.Table.Any(meta => meta.PostId == postId && meta.MetaKey == PostMetaKey.VISITS))
                {
                    _postMetaRepository.Add(new PostMeta
                    {
                        PostId = postId,
                        MetaKey = PostMetaKey.VISITS,
                        MetaValue = 0.ToString()
                    });
                }

                var sql = 
                    "UPDATE public.\"PostMeta\"" +
                    "   SET \"MetaValue\"=(\"MetaValue\"::Integer)+1" +
                    "   WHERE \"MetaKey\"=@MetaKey" +
                    "       AND \"PostId\"=@PostId;";

                _context.Database.ExecuteSqlCommand(
                    sql, 
                    new NpgsqlParameter("@MetaKey", PostMetaKey.VISITS), 
                    new NpgsqlParameter("@PostId", postId));
            }
        }
    }
}

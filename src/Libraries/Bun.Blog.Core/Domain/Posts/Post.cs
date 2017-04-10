namespace Bun.Blog.Core.Domain.Posts
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }

        public string Excerpt { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }

        public int Status { get; set; }
    }
}

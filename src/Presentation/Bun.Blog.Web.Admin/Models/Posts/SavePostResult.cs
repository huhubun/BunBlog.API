namespace Bun.Blog.Web.Admin.Models.Posts
{
    public class SavePostResult
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url
        {
            get
            {
                return $"http://localhost/post/{Id}";
            }
        }
    }
}

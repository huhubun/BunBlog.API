namespace BunBlog.API.Models.SiteLinks
{
    public class SiteLinkModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 站点链接
        /// </summary>
        public string Link { get; set; }
    }
}

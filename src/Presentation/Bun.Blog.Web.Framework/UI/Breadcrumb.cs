using System.Collections.Generic;

namespace Bun.Blog.Web.Framework.UI
{
    public static class Breadcrumb
    {
        public static List<BreadcrumbItem> Add(string name, string url)
        {
            return CreateFirst(new BreadcrumbItem
            {
                Name = name,
                Url = url,
                IsCurrent = false
            });
        }

        public static List<BreadcrumbItem> Current(string name)
        {
            return CreateFirst(new BreadcrumbItem
            {
                Name = name,
                IsCurrent = true
            });
        }

        public static List<BreadcrumbItem> Add(this List<BreadcrumbItem> breadcrumbItems, string name, string url)
        {
            breadcrumbItems.Add(new BreadcrumbItem
            {
                Name = name,
                Url = url,
                IsCurrent = false
            });

            return breadcrumbItems;
        }

        public static List<BreadcrumbItem> Current(this List<BreadcrumbItem> breadcrumbItems, string name)
        {
            breadcrumbItems.Add(new BreadcrumbItem
            {
                Name = name,
                IsCurrent = true
            });

            return breadcrumbItems;
        }

        private static List<BreadcrumbItem> CreateFirst(BreadcrumbItem item)
        {
            return new List<BreadcrumbItem> { item };
        }
    }
}

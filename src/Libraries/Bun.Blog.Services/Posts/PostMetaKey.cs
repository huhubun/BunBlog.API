using System;
using System.Collections.Generic;
using System.Linq;

namespace Bun.Blog.Services.Posts
{
    public static class PostMetaKey
    {
        public const string VISITS = "Visits";

        private static List<string> metaKeyList = new List<string> {
            VISITS
        };

        public static bool IsKey(string metaKey)
        {
            if (String.IsNullOrEmpty(metaKey))
            {
                return false;
            }

            return metaKeyList.Any(n => n.ToUpper() == metaKey.ToUpper());
        }
    }
}

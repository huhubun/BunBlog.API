using System;

namespace BunBlog.Core.Exceptions
{
    public class BunBlogConfigException : Exception
    {
        public BunBlogConfigException(string configSectionName) : base()
        {
            ConfigSectionName = configSectionName;
        }

        public BunBlogConfigException(string configSectionName, string message) : base(message)
        {
            ConfigSectionName = configSectionName;
        }

        public BunBlogConfigException(string configSectionName, string message, Exception innerException) : base(message, innerException)
        {
            ConfigSectionName = configSectionName;
        }

        public string ConfigSectionName { get; set; }
    }
}

using System.Text.RegularExpressions;

namespace BunBlog.Core
{
    public static class BunHelper
    {
        /// <summary>
        /// 从 RuntimeInformation.OSDescription 中获取系统发行版本名称
        /// </summary>
        /// <param name="osDescription">RuntimeInformation.OSDescription 的返回值</param>
        /// <returns>系统发行版本名称</returns>
        public static string GetOSName(string osDescription)
        {
            if (osDescription.StartsWith("Microsoft Windows"))
            {
                return "Windows";
            }

            if (new Regex("^Linux.*Debian").IsMatch(osDescription))
            {
                return "Debian";
            }

            return "Other";
        }
    }
}

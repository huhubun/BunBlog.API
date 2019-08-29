using System.Reflection;

namespace BunBlog.API.Models.Informations
{
    public class InformationModel
    {
        /// <summary>
        /// 当前程序的版本号
        /// </summary>
        public string Version
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;

                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }
    }
}

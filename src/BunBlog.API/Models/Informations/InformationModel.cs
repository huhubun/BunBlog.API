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
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }
}

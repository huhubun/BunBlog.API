using BunBlog.Core;
using System.Reflection;
using System.Runtime.InteropServices;

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

        /// <summary>
        /// 运行程序的操作系统
        /// </summary>
        public string OS
        {
            get
            {
                return BunHelper.GetOSName(RuntimeInformation.OSDescription);
            }
        }

        /// <summary>
        /// 运行程序的运行时描述
        /// </summary>
        public string RuntimeFramework
        {
            get
            {
                return RuntimeInformation.FrameworkDescription;
            }
        }
    }
}

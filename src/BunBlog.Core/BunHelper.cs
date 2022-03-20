using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.IO;

namespace BunBlog.Core;

public static class BunHelper
{
    /// <summary>
    /// 从 RuntimeInformation.OSDescription 中获取系统发行版本名称
    /// </summary>
    /// <param name="osDescription"><see cref="RuntimeInformation.OSDescription"/></param>
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

    public static bool CheckKubernetesServiceAccountDirectoryExists() => Directory.Exists("/var/run/secrets/kubernetes.io/serviceaccount");
}

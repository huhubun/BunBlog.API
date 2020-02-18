using BunBlog.Core.Domain.Configurations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BunBlog.Services.Configurations
{
    public interface IConfigurationService
    {
        /// <summary>
        /// 获取所有配置项的定义
        /// </summary>
        /// <returns></returns>
        Task<List<ConfigurationResourceItem>> GetConfigurationResourcesAsync();

        /// <summary>
        /// 根据配置代码获取配置项的定义
        /// </summary>
        /// <param name="code">配置代码</param>
        /// <returns></returns>
        Task<ConfigurationResourceItem> GetConfigurationResourceByCodeAsync(string code);

        /// <summary>
        /// 验证配置是否符合定义的要求
        /// </summary>
        /// <param name="configuration">要验证的配置</param>
        /// <param name="configurationResource">配置项定义</param>
        /// <returns></returns>
        ConfigurationVerifyResult Verify(string value, ConfigurationResourceItem configurationResource);

        /// <summary>
        /// 获取所有配置项
        /// </summary>
        /// <returns></returns>
        Task<List<Configuration>> GetListAsync();

        /// <summary>
        /// 根据配置代码获取单个配置
        /// </summary>
        /// <param name="code">配置代码</param>
        /// <param name="tracking">是否跟踪</param>
        /// <returns></returns>
        Task<Configuration> GetByCodeAsync(string code, bool tracking = false);

        Task<Configuration> AddAsync(Configuration configuration);

        Task<Configuration> EditAsync(Configuration configuration);
    }
}

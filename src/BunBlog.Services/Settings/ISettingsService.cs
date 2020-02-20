using BunBlog.Core.Domain.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BunBlog.Services.Settings
{
    public interface ISettingsService
    {
        /// <summary>
        /// 获取所有配置项的定义
        /// </summary>
        /// <returns></returns>
        Task<List<SettingDefinition>> GetDefinitionsAsync();

        /// <summary>
        /// 根据配置代码获取配置项的定义
        /// </summary>
        /// <param name="code">配置代码</param>
        /// <returns></returns>
        Task<SettingDefinition> GetDefinitionByCodeAsync(string code);

        /// <summary>
        /// 验证配置是否符合定义的要求
        /// </summary>
        /// <param name="value">要验证的值</param>
        /// <param name="definition">定义</param>
        /// <returns></returns>
        SettingsVerifyResult Verify(string value, SettingDefinition definition);

        /// <summary>
        /// 获取所有配置项
        /// </summary>
        /// <returns></returns>
        Task<List<Setting>> GetListAsync();

        /// <summary>
        /// 根据配置代码获取单个配置
        /// </summary>
        /// <param name="code">配置代码</param>
        /// <param name="tracking">是否跟踪</param>
        /// <returns></returns>
        Task<Setting> GetByCodeAsync(string code, bool tracking = false);

        Task<Setting> AddAsync(Setting setting);

        Task<Setting> EditAsync(Setting setting);
    }
}

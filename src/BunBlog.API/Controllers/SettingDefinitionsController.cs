using BunBlog.API.Models.Settings;
using BunBlog.Services.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.API.Controllers
{
    [Route("api/settingDefinitions")]
    [ApiController]
    public class SettingDefinitionsController : ControllerBase
    {
        private readonly ISettingsService _settingService;

        public SettingDefinitionsController(
            ISettingsService settingService)
        {
            _settingService = settingService;
        }

        /// <summary>
        /// 获取配置信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> GetListAsync()
        {
            var settingDefinitionsTask = _settingService.GetDefinitionsAsync();
            var settingsTask = _settingService.GetListAsync();

            var settingDefinition = await settingDefinitionsTask;
            var settings = await settingsTask;

            var models = from _d in settingDefinition
                         join _s in settings on _d.Code equals _s.Code into ss
                         from si in ss.DefaultIfEmpty()
                         select new SettingDefinitionWithValueModel
                         {
                             Code = _d.Code,
                             Category = _d.Category,
                             Type = _d.Type,
                             ValueType = _d.ValueType,
                             Schema = _d.Schema,
                             AllowNull = _d.AllowNull,
                             DefaultValue = _d.DefaultValue,
                             Description = _d.Description,
                             Value = si?.Value
                         };

            return Ok(models);
        }
    }
}

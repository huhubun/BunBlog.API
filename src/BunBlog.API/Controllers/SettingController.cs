using BunBlog.API.Const;
using BunBlog.API.Models;
using BunBlog.API.Models.Settings;
using BunBlog.Core.Domain.Settings;
using BunBlog.Services.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.API.Controllers
{
    /// <summary>
    /// 配置
    /// </summary>
    [Route("api/settings")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(
            ISettingService settingService
            )
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

            var result = from _d in settingDefinition
                         join _s in settings on _d.Code equals _s.Code into ss
                         from si in ss.DefaultIfEmpty()
                         select new SettingDefinitionWithValueModel
                         {
                             Code = _d.Code,
                             Category = _d.Category,
                             Type = _d.Type,
                             ValueType = _d.ValueType,
                             Description = _d.Description,
                             Value = si?.Value
                         };

            return Ok(result);
        }

        /// <summary>
        /// 修改指定配置项的值
        /// </summary>
        /// <param name="code">配置项代码</param>
        /// <param name="request">修改配置项请求</param>
        /// <returns></returns>
        [HttpPut("{code}")]
        [Authorize]
        public async Task<IActionResult> EditByCode([FromRoute]string code, [FromBody]EditSettingRequest request)
        {
            var definition = await _settingService.GetDefinitionByCodeAsync(code);
            if (definition == null)
            {
                return BadRequest(new ErrorResponse(ErrorResponseCode.INVALID_CODE, $"code \"{code}\" 无效，没有定义该配置项"));
            }

            var verifyResult = _settingService.Verify(request.Value, definition);
            if (!verifyResult.IsVerify)
            {
                return BadRequest(new ErrorResponse(ErrorResponseCode.INVALID_VALUE, $"value 校验失败：{verifyResult.Message}"));
            }

            var setting = await _settingService.GetByCodeAsync(code, true);

            // 数据库中不存在这个配置
            if (setting == null)
            {
                setting = new Setting
                {
                    Code = code,
                    Value = request.Value
                };

                await _settingService.AddAsync(setting);
            }
            else
            {
                setting.Value = request.Value;

                await _settingService.EditAsync(setting);
            }

            return NoContent();
        }
    }
}
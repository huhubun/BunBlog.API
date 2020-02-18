using BunBlog.API.Const;
using BunBlog.API.Models;
using BunBlog.API.Models.Configurations;
using BunBlog.Core.Domain.Configurations;
using BunBlog.Services.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.API.Controllers
{
    /// <summary>
    /// 配置
    /// </summary>
    [Route("api/configurations")]
    [ApiController]
    public class ConfigurationsController : ControllerBase
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationsController(
            IConfigurationService configurationService
            )
        {
            _configurationService = configurationService;
        }

        /// <summary>
        /// 获取配置信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> GetListAsync()
        {
            var resourcesTask = _configurationService.GetConfigurationResourcesAsync();
            var savedConfigurationTask = _configurationService.GetListAsync();

            var resources = await resourcesTask;
            var savedConfiguration = await savedConfigurationTask;

            var result = from _r in resources
                         join _c in savedConfiguration on _r.Code equals _c.Code into cs
                         from c in cs.DefaultIfEmpty()
                         select new ConfigurationResourceItemWithValueModel
                         {
                             Code = _r.Code,
                             Category = _r.Category,
                             Type = _r.Type,
                             ValueType = _r.ValueType,
                             Description = _r.Description,
                             Value = c?.Value
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
        public async Task<IActionResult> EditByCode([FromRoute]string code, [FromBody]EditConfigurationRequest request)
        {
            var configurationResource = await _configurationService.GetConfigurationResourceByCodeAsync(code);
            if (configurationResource == null)
            {
                return BadRequest(new ErrorResponse(ErrorResponseCode.INVALID_CODE, $"code \"{code}\" 无效，没有定义该配置项"));
            }

            var verifyResult = _configurationService.Verify(request.Value, configurationResource);
            if (!verifyResult.IsVerify)
            {
                return BadRequest(new ErrorResponse(ErrorResponseCode.INVALID_CONFIGURATION_VALUE, $"value 校验失败：{verifyResult.Message}"));
            }

            var configuration = await _configurationService.GetByCodeAsync(code, true);

            // 数据库中不存在这个配置
            if (configuration == null)
            {
                configuration = new Configuration
                {
                    Code = code,
                    Value = request.Value
                };

                await _configurationService.AddAsync(configuration);
            }
            else
            {
                configuration.Value = request.Value;

                await _configurationService.EditAsync(configuration);
            }

            return NoContent();
        }
    }
}
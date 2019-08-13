using BunBlog.API.Models.Informations;
using Microsoft.AspNetCore.Mvc;

namespace BunBlog.API.Controllers
{
    [Route("api/information")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        /// <summary>
        /// 获取 Bun Blog API 程序信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(InformationModel), 200)]
        public IActionResult GetInformation()
        {
            return Ok(new InformationModel());
        }
    }
}
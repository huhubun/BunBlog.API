using AutoMapper;
using BunBlog.API.Models.SiteLinks;
using BunBlog.Core.Domain.SiteLinks;
using BunBlog.Services.SiteLinks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BunBlog.API.Controllers
{
    [Route("api/siteLinks")]
    [ApiController]
    public class SiteLinkController : ControllerBase
    {
        private readonly ISiteLinkService _siteLinkService;
        private readonly IMapper _mapper;

        public SiteLinkController(
            ISiteLinkService siteLinkService,
            IMapper mapper
            )
        {
            _siteLinkService = siteLinkService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetSiteLinkListAsync()
        {
            var list = await _siteLinkService.GetListAsync();
            return Ok(_mapper.Map<List<SiteLinkModel>>(list));
        }

        [HttpGet("{id:int}", Name = nameof(GetSiteLinkByIdAsync))]
        public async Task<IActionResult> GetSiteLinkByIdAsync([FromRoute]int id)
        {
            var siteLink = await _siteLinkService.GetByIdAsync(id);

            if (siteLink == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SiteLinkModel>(siteLink));
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> AddSiteLinkAsync([FromBody]SiteLinkModel siteLinkModel)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var siteLink = _mapper.Map<SiteLink>(siteLinkModel);
            await _siteLinkService.AddAsync(siteLink);

            return CreatedAtRoute(nameof(GetSiteLinkByIdAsync), new { id = siteLink.Id }, _mapper.Map<SiteLinkModel>(siteLink));
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> EditSiteLinkAsync([FromRoute]int id, [FromBody]SiteLinkModel siteLinkModel)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var siteLink = await _siteLinkService.GetByIdAsync(id);
            if (siteLink == null)
            {
                return NotFound();
            }

            siteLink = _mapper.Map(siteLinkModel, siteLink);
            await _siteLinkService.EditAsync(siteLink);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteSiteLinkByIdAsync([FromRoute]int id)
        {
            var siteLink = await _siteLinkService.GetByIdAsync(id);
            if (siteLink == null)
            {
                return NotFound();
            }

            await _siteLinkService.DeleteAsync(siteLink);

            return NoContent();
        }
    }
}
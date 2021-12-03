using Blog.Application.Dtos.Commons;
using Blog.Application.Queries.Settings;
using Blog.Shared.Consts.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.EndPoint.Controllers
{

    public class CommandController : BaseController
    {
        #region Fields
        private readonly IMediator _mediator;


        #endregion
        #region Constructors
        public CommandController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion
        #region Actions
        [HttpGet("GetTopOfSiteInfo")]
        public async Task<IActionResult> GetTopOfSiteInfo()
        {
            var settings = await _mediator.Send(new GetSettingsQuery());
            var result = new GetTopOfSiteInfoDto
            {
                Email = settings.FirstOrDefault(c => c.Key.Equals(SettingConsts.Email))?.Value,
                MobileNumber = settings.FirstOrDefault(c => c.Key.Equals(SettingConsts.MobileNumber))?.Value

            };
            return Ok(result);
        }

        [HttpGet("GetLogo")]
        public async Task<IActionResult> GetLogo()
        {
            var settings = await _mediator.Send(new GetSettingsQuery());
            var result = new LogoDto
            {
                Logo = settings.FirstOrDefault(c => c.Key.Equals(SettingConsts.Logo))?.Value,
                LogoThumbnail = settings.FirstOrDefault(c => c.Key.Equals(SettingConsts.LogoThumbnail))?.Value
            };
            return Ok(result);
        }

        #endregion
    }
}

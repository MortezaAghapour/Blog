using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Commands.SocialNetworks.Create;
using Blog.Application.Commands.SocialNetworks.Delete;
using Blog.Application.Commands.SocialNetworks.Update;
using Blog.Application.Queries.SocialNetworks;
using MediatR;

namespace Blog.EndPoint.Controllers
{

    public class SocialNetworkController : BaseController
    {
        #region Fields

        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public SocialNetworkController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Actions

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSocialNetwork(CreateSocialNetworkCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("SocialNetworks")]
        public async Task<IActionResult> GetSocialNetworks()
        {
            var result = await _mediator.Send(new GetSocialNetworksQuery());
            return Ok(result);
        }

        [HttpPut("EditSocialNetwork")]
        public async Task<IActionResult> UpdateSocialNetwork(UpdateSocialNetworkCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("RemoveSocialNetwork/{id:long}")]
        public async Task<IActionResult> DeleteSocialNetwork(long id)
        {
            var result = await _mediator.Send(new DeleteSocialNetworkCommand { Id = id });
            return Ok(result);
        }

        #endregion
    }
}

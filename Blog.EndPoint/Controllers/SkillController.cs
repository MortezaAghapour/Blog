using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Blog.Application.Commands.Skills.Create;
using Blog.Application.Commands.Skills.Delete;
using Blog.Application.Commands.Skills.Update;
using Blog.Application.Queries.Categories;
using MediatR;

namespace Blog.EndPoint.Controllers
{

    public class SkillController : BaseController
    {
        #region Fields

        private readonly IMediator _mediator;


        #endregion

        #region Constructors
        public SkillController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Actions

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSkill(CreateSkillCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("Skills")]
        public async Task<IActionResult> GetSkills()
        {
            var result = await _mediator.Send(new GetSkillsQuery());
            return Ok(result);
        }

        [HttpPut("EditSkill")]
        public async Task<IActionResult> UpdateSkill(UpdateSkillCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("RemoveSkill/{id:long}")]
        public async Task<IActionResult> DeleteSkill(long id)
        {
            var result = await _mediator.Send(new DeleteSkillCommand { Id = id });
            return Ok(result);
        }

        #endregion
    }
}

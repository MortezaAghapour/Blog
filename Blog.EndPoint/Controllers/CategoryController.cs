using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Commands.Categories.Create;
using Blog.Application.Commands.Categories.Delete;
using Blog.Application.Commands.Categories.Update;
using Blog.Application.Queries.Categories;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Blog.EndPoint.Controllers
{

    public class CategoryController : BaseController
    {
        #region Fields
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Actions

        [HttpPost("CreateCategory")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("Categories")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            var result =await _mediator.Send(new GetCategoriesQuery());
            return Ok(result);
        }

        [HttpPut("EditCategory")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command)
        {
            var result =await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("RemoveCategory/{id:long}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand{Id = id});
            return Ok(result);
        }

        #endregion
    }
}

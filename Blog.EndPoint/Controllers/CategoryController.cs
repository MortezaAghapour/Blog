using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Commands.Categories.Create;
using MediatR;

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

        #endregion
    }
}

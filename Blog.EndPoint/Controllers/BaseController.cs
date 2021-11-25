using Blog.Infrastructure.Attributes.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.EndPoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiResult]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController :  ControllerBase
    {

    }
}
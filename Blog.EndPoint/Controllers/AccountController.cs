using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Blog.EndPoint.Infrastructure.Extensions.ModelsStates;
using Blog.Infrastructure.Identity.Dtos;
using Blog.Infrastructure.Identity.Services.Users;
using Microsoft.AspNetCore.Authorization;

namespace Blog.EndPoint.Controllers
{

    public class AccountController : BaseController
    {
        #region Fields

        private readonly IUserService _userService;


        #endregion
        #region Constructors

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion
        #region Actions
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    ModelState.GetModelStateErrors()
                );
            var login = await _userService.Login(model);
            if (login.IsSuccess)
            {
                return Ok(login.Data);
            }
            else
            {
                return BadRequest(login);
            }
        }
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    ModelState.GetModelStateErrors()
                );
            var register = await _userService.Register(model);
            if (register.IsSuccess)
            {
                return Ok(register.Data);
            }
            else
            {
                return BadRequest(register);
            }
        }
        #endregion

    }
}

using System;
using Matrix.Core.Commands;
using Matrix.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Matrix.Ui.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public AccountController(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterInputVm model)
        {
            var res = await _mediator.Send(new RegisterInputCommand(model.Email , model.Password));
            return Ok(res);

        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginInputVm model)
        {

            var result = await _mediator.Send(new LoginInputQuery
            {
                Email = model.Email,
                Password = model.Password
            });
            return Ok(result);
        }

        [HttpGet("test")]
        [Authorize]
        public async Task<IActionResult> Test()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            return Ok("success " + userId);

        }



    }
}


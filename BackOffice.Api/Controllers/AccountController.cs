using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackOffice.Application.Dtos;
using BackOffice.Domain.Entities;
using BackOffice.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BackOffice.Api.Controllers
{
    [ApiController]
    [Route("/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        //private readonly login<User> signInManager;
        public readonly IConfiguration configuration;
        //private readonly IUnitOfWork unitOfWork;
        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }


        //public AccountController(IUnitOfWork unitOfWor)
        //   //UserManager<User> userManager,
        //   //SignInManager<User> signInManager, IConfiguration configuration)

        //{
        //    this.unitOfWork = unitOfWork;
        //}


        //[HttpPost]
        //public async Task<IActionResult> CreateUser(CreateUserDto dto)
        //{

        //    return Ok(await unitOfWork.UserRepository.AddUserAsync(dto));

        //}


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.FindByNameAsync(Model.Email);
            if (user == null)
            {
                return BadRequest(new { message = "Email İs incorrect" });
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, Model.Password, false);
            if (result.Succeeded)
            {
                return Ok(new { Token = GenerateJwtToken(user) });
            }
            else
                return Unauthorized();
        }

        private string GenerateJwtToken(User user)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSetting:Secret").Value);
            var TokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = TokenHandler.CreateToken(TokenDescriptor);
            return TokenHandler.WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<string> Register(CreateUserDto model)
        {
            string res  ="hasErrror ";
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                };


                // добавляем пользователя
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    //await userManager (user, false);
                     res = GenerateJwtToken(user);

                    return res;
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return res;
        }


        //[HttpPost("LoginTo")]
        //public async Task<IActionResult> LoginTo(LoginDto dto)
        //{
        //    return Ok(await unitOfWork.UserRepository.Login(dto.Email, dto.Password));
        //}
    }
}


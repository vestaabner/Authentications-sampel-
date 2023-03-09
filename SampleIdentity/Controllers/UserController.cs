using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleIdentity.Dtos;
using SampleIdentity.Entites;

namespace SampleIdentity.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        //private readonly IJWTAuthenticationManagerr _jWT;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto item)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);


            User user = new User
            {
                UserName = item.Email,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email
            };


            var existUser = await _userManager.FindByEmailAsync(item.Email);
            if (existUser == null)
            {
                var result = await _userManager.CreateAsync(user, item.PassWord);


                if (result.Succeeded)
                {
                    //var token = _jWT.Authenticate(user);
                    //if (token == null)
                    //{
                    //    return null;
                    //}
                    //var UsrFromDb = await _userManager.FindByNameAsync(item.UserName);
                    //await _userManager.AddToRoleAsync(UsrFromDb, "user");
                    //return new AccessToken() { Token = token, ExpiredAt = DateTime.Now.AddHours(3) };
                    return Ok();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }



        [HttpGet]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto item)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var user = _userManager.FindByNameAsync(item.UserName);

            if (user?.Result == null)
                return BadRequest(ModelState);

            var loginUser = await _signInManager.PasswordSignInAsync(user.Result, item.PassWord, item.IsPercitance, true);

            if(loginUser.Succeeded == true)
            {
                return Ok(item.ReturnUrl);
            }
            if (loginUser.RequiresTwoFactor == true)
            {
                return Ok(item.ReturnUrl);
            }
            else
            {
                return BadRequest(ModelState);

            }
        }

    }
}

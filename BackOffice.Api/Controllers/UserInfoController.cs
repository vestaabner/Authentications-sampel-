using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserInfoController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            var userId = User.Identity.Name;
            return userId;
        }
    }
}

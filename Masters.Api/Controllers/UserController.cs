using Masters.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Masters.Api.Controllers
{
    public class UserController : Controller
    {
        private IConfiguration config;
        private IOptions<Authentication> authSettings;

        public UserController(IConfiguration _config, IOptions<Authentication> _authSettings)
        {
            config = _config;
            authSettings = _authSettings;
        }

        [HttpPost("/Login")]
        public IActionResult Login()
        {
            return Ok(authSettings.Value.Jwt.SecurityKey);
        }
    }
}

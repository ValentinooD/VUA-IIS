using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InfoController : ControllerBase
    {

        [HttpGet]
        public ActionResult Get()
        {
            string username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok("Hello, you are authenticated as " + username);
        }
    }
}

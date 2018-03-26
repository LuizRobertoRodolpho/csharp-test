using Microsoft.AspNetCore.Mvc;
using PortalTelemedicina.DomainService;
using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository;
using PortalTelemedicina.Repository.Entities;

namespace PortalTelemedicina.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class LoginController : Controller
    {
        IUserDomainService _domainService;

        public LoginController(ApplicationContext context)
        {
            _domainService = new UserDomainService(context);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult SignIn(string username, string password)
        {
            return BadRequest("Not implemented.");
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult SignUp([FromBody]User value)
        {
            try
            {
                var success = _domainService.Create(value);

                if (success)
                    return Ok("User created.");
                else
                    return NotFound();
            }
            catch
            {
                return BadRequest("Unable to create the user.");
            }
        }
    }
}
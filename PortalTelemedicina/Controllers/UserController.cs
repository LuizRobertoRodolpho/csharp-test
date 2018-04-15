using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalTelemedicina.DomainService;
using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository;
using PortalTelemedicina.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace PortalTelemedicina.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class UserController : Controller
    {
        private IUserDomainService _domainService;

        public UserController(ApplicationContext context)
        {
            _domainService = new UserDomainService(context);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<IActionResult> Users(UserSearchViewModel data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var users = await _domainService.Get(data.UserName, data.DisplayName, data.StartDate, data.EndDate, data.Email);
                var query = from user in users
                            select new
                            {
                                userId = user.UserId,
                                userName = user.UserName,
                                displayName = user.DisplayName,
                                email = user.Email,
                                creationDate = user.CreationDate.ToString("dd/MM/yyyy HH:mm:ss")
                            };

                return new OkObjectResult(query);
            }
            catch
            {
                return BadRequest("Unable to perform the search.");
            }
        }
    }
}
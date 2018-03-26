using Microsoft.AspNetCore.Mvc;
using PortalTelemedicina.DomainService;
using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository;
using System;

namespace PortalTelemedicina.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class UserController : Controller
    {
        IUserDomainService _domainService;

        public UserController(ApplicationContext context)
        {
            _domainService = new UserDomainService(context);
        }

        [HttpGet]
        [Route("[action]")]
        public JsonResult Users(string username = null, string displayname = null,
                                DateTime? startDate = null, DateTime? endDate = null, string email = null)
        {
            return new JsonResult(_domainService.Get(username, displayname, startDate, endDate, email));
        }
    }
}
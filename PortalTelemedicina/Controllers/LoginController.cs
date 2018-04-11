using Microsoft.AspNetCore.Mvc;
using PortalTelemedicina.DomainService;
using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository;
using PortalTelemedicina.Repository.Entities;
using RestSharp;
using System;
using System.Collections.Generic;

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
        public IActionResult SignIn([FromBody]User value)
        {
            try
            {
                var success = _domainService.Get(value);

                if (success)
                {
                    // retrieve api token
                    var client = new RestClient("https://elune.auth0.com/oauth/token");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/json");
                    request.AddParameter("application/json", "{\"client_id\":\"qmv7hHYkQSkq1QIfQ5k8leXO1cPOvgU3\",\"client_secret\":\"wQBEp6oPuhlrcldZn-f-sgueoC1GSec1tkULhUz3ULruZ2w0ZgaN0l6SXeLJ97hh\",\"audience\":\"https://elune.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    var responseDictionary = new RestSharp.Deserializers.JsonDeserializer().Deserialize<Dictionary<string, string>>(response);

                    return Ok(responseDictionary["access_token"]);
                }
                else
                {
                    return NotFound("User not found, check the fields.");
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
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
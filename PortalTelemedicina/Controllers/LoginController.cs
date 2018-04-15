using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PortalTelemedicina.DomainService;
using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository;
using PortalTelemedicina.Repository.Entities;
using PortalTelemedicina.ViewModel;
using RestSharp;
using System;
using System.Collections.Generic;

namespace PortalTelemedicina.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class LoginController : Controller
    {
        private IUserDomainService _domainService;
        private readonly IOptions<Auth0Config> config;

        public LoginController(ApplicationContext context, IOptions<Auth0Config> config)
        {
            this.config = config;
            _domainService = new UserDomainService(context);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult SignIn([FromBody]SignInViewModel value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var success = _domainService.Get(value.UserName, value.Password);

                if (success)
                {
                    // retrieve Auth0 api token
                    var client = new RestClient(config.Value.TokenAuthAddress);
                    var request = new RestRequest(Method.POST);
                    var headerContent = $"{{\"client_id\":\"{config.Value.ClientId}\",\"client_secret\":\"{config.Value.ClientSecret}\",\"audience\":\"{config.Value.ApiIdentifier}\",\"grant_type\":\"client_credentials\"}}";
                    request.AddHeader("content-type", "application/json");
                    request.AddParameter("application/json", headerContent, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    var responseDictionary = new RestSharp.Deserializers.JsonDeserializer().Deserialize<Dictionary<string, string>>(response);

                    return Ok(responseDictionary["access_token"]);
                }
                else
                {
                    return NotFound("Authentication failed!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult SignUp([FromBody]SignUpViewModel value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var success = _domainService.Create(new User
                {
                    DisplayName = value.DisplayName,
                    Email = value.Email,
                    UserName = value.UserName,
                    Password = value.Password
                });

                if (success)
                    return Ok("User created.");
                else
                    return BadRequest("Unable to create the user.");
            }
            catch (Exception ex)
            {
                return BadRequest("Unable to create the user. " + ex.Message);
            }
        }
    }
}
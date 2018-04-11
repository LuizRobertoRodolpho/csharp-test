using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalTelemedicina.DomainService;
using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository;
using PortalTelemedicina.Repository.Entities;
using System;

namespace PortalTelemedicina.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class ProductController : Controller
    {
        IProductDomainService _domainService;

        public ProductController(ApplicationContext context)
        {
            _domainService = new ProductDomainService(context);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public IActionResult Products(string name = null, string description = null, decimal? price = null,
                                DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                return new JsonResult(_domainService.Get(name, description, price, startDate, endDate));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[controller]")]
        [Authorize]
        public IActionResult Create([FromBody]Product value)
        {
            try
            {
                _domainService.Create(value.Name, value.Description, value.Price);

                return Ok("Product created.");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("[controller]/{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody]Product value)
        {
            try
            {
                var success = _domainService.Update(id, value.Name, value.Description, value.Price);

                if (success)
                    return Ok("Product updated.");
                else
                    return NotFound("Product not found.");
            }
            catch
            {
                return BadRequest("Unable to update the product.");
            }
        }
    }
}
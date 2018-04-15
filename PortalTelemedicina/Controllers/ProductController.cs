﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalTelemedicina.DomainService;
using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository;
using PortalTelemedicina.ViewModel;
using System.Linq;

namespace PortalTelemedicina.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class ProductController : Controller
    {
        private IProductDomainService _domainService;

        public ProductController(ApplicationContext context)
        {
            _domainService = new ProductDomainService(context);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public IActionResult Products(ProductSearchViewModel data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var products = _domainService.Get(data.Name, data.Description, data.Price, data.StartDate, data.EndDate);
                var query = from product in products
                            select new
                            {
                                productId = product.ProductId,
                                name = product.Name,
                                description = product.Description,
                                price = product.Price.ToString("0.00"),
                                creationDate = product.CreationDate.ToString("dd/MM/yyyy HH:mm:ss")
                            };

                return new OkObjectResult(query);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[controller]")]
        [Authorize]
        public IActionResult Create([FromBody]ProductCreateViewModel data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _domainService.Create(data.Name, data.Description, data.Price);

                return Ok("Product created.");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("[controller]/{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody]ProductUpdateViewModel value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

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
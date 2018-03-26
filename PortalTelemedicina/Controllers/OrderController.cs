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
    public class OrderController : Controller
    {
        IOrderDomainService _domainService;

        public OrderController(ApplicationContext context)
        {
            _domainService = new OrderDomainService(context);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Orders(int? orderId = null, int? userId = null,
            DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var orders = _domainService.Get(orderId, userId, startDate, endDate);

                return new JsonResult(orders);
            }
            catch
            {
                return BadRequest("Unable to retrieve orders.");
            }
        }

        [HttpPost]
        [Route("[controller]")]
        public IActionResult Create([FromBody]Order order)
        {
            try
            {
                var success = _domainService.Create(order);

                if (success)
                    return Ok("Order created.");
                else
                    return NotFound("User or product not found.");
            }
            catch
            {
                return BadRequest("Unable to create the order.");
            }
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalTelemedicina.DomainService;
using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository;
using PortalTelemedicina.Repository.Entities;
using PortalTelemedicina.ViewModel;
using System;
using System.Linq;

namespace PortalTelemedicina.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class OrderController : Controller
    {
        private IOrderDomainService _domainService;

        public OrderController(ApplicationContext context)
        {
            _domainService = new OrderDomainService(context);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public IActionResult Orders(OrderSearchViewModel data)
        {
            try
            {
                var orders = _domainService.Get(data.OrderId, data.UserId, data.StartDate, data.EndDate, data.MinTotal, data.MaxTotal);

                return new OkObjectResult(orders);
            }
            catch
            {
                return BadRequest("Unable to retrieve orders.");
            }
        }

        [HttpPost]
        [Route("[controller]")]
        [Authorize]
        public IActionResult Create([FromBody]OrderCreateViewModel order)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var success = _domainService.Create(new Order
                {
                    UserId = order.UserId,
                    OrderItems = order.OrderItems.Select(x => new OrderItem
                    {
                        ProductId = x.ProductId,
                        Amount = x.Amount,
                        CurrentPrice = x.CurrentPrice
                    }).ToList()
                });

                if (success)
                    return Ok("Order created.");
                else
                    return NotFound("User or product not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
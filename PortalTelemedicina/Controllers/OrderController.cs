﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalTelemedicina.DomainService;
using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository;
using PortalTelemedicina.Repository.Entities;
using PortalTelemedicina.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Orders(OrderSearchViewModel data)
        {
            try
            {
                var orders = await _domainService.Get(data.OrderId, data.UserId, data.StartDate, data.EndDate, data.MinTotal, data.MaxTotal);

                var viewModelList = orders.Select(x => new OrderResultViewModel
                {
                    OrderId = x.OrderId,
                    CreationDate = x.CreationDate,
                    OrderItems = x.OrderItems.Select(y => new OrderItemResultViewModel
                                                            {
                                                                OrderItemId = y.OrderItemId,
                                                                ProductId = y.ProductId,
                                                                Amount = y.Amount,
                                                                CurrentPrice = y.CurrentPrice
                                                            }).ToList()
                }).ToList();

                return new OkObjectResult(viewModelList);
            }
            catch
            {
                return BadRequest("Unable to retrieve orders.");
            }
        }

        [HttpPost]
        [Route("[controller]")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody]OrderCreateViewModel order)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var success = await _domainService.Create(new Order
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
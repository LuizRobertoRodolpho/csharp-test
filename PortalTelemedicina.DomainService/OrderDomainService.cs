using Microsoft.EntityFrameworkCore;
using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository;
using PortalTelemedicina.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalTelemedicina.DomainService
{
    public class OrderDomainService : IOrderDomainService
    {
        private ApplicationContext context;

        public OrderDomainService(ApplicationContext _context)
        {
            context = _context;
        }

        public async Task<List<Order>> Get(int? orderId, int? userId, DateTime? startDate, DateTime? endDate, decimal? minTotal, decimal? maxTotal)
        {
            var ordersQuery = context.Orders.AsQueryable();

            if (orderId.HasValue)
                ordersQuery = ordersQuery.Where(x => x.OrderId == orderId.Value);

            if (userId.HasValue)
                ordersQuery = ordersQuery.Where(x => x.UserId == userId.Value);

            if (startDate.HasValue)
                ordersQuery = ordersQuery.Where(x => x.CreationDate >= startDate);

            if (endDate.HasValue)
                ordersQuery = ordersQuery.Where(x => x.CreationDate <= endDate);

            // orders with at total minimum value
            if (minTotal.HasValue)
            {
                ordersQuery = ordersQuery.Where(x => x.OrderItems.Sum(s => s.CurrentPrice * s.Amount) >= minTotal.Value);
            }

            // orders with at total maximum value
            if (maxTotal.HasValue)
            {
                ordersQuery = ordersQuery.Where(x => x.OrderItems.Sum(s => s.CurrentPrice * s.Amount) <= maxTotal.Value);
            }

            // get order items -- can be updated to get automatically
            var ordersList = await ordersQuery.ToListAsync();

            foreach (var o in ordersList)
            {
                o.OrderItems = context.OrderItems.Where(x => x.OrderId == o.OrderId).ToList();
            }

            return ordersList;
        }

        public async Task<bool> Create(Order order)
        {
            if (await context.Users.AnyAsync(x => x.UserId == order.UserId) == false)
            {
                throw new Exception("User not found!");
            }

            if (order.OrderItems.Count == 0)
            {
                throw new Exception("The order requires at least on product!");
            }

            // check if all products exists in the database
            var missingProducts = order.OrderItems.Where(x => !context.Products
                                                                      .Where(y => y.ProductId == x.ProductId)
                                                                      .Any());

            if (missingProducts.Count() > 0)
            {
                var ids = string.Join(", ", missingProducts.Select(x => x.ProductId));
                throw new Exception("The following products was not found: " + ids);
            }

            var newOrder = new Order();
            newOrder.UserId = order.UserId;
            newOrder.CreationDate = DateTime.Now;

            newOrder.OrderItems = order.OrderItems.Where(x => context.Products
                                                                     .Where(y => y.ProductId == x.ProductId).Any())
                                                                     .Select(oitem => new OrderItem
                                                                     {
                                                                         OrderId = newOrder.OrderId,
                                                                         ProductId = oitem.ProductId,
                                                                         CurrentPrice = oitem.CurrentPrice,
                                                                         Amount = oitem.Amount
                                                                     }).ToList();

            if (newOrder.OrderItems.Count > 0)
            {
                await context.Orders.AddAsync(newOrder);

                return await context.SaveChangesAsync() > 0;
            }
            else
            {
                return false;
            }
        }
    }
}

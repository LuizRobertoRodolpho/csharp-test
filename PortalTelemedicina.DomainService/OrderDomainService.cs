using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository;
using PortalTelemedicina.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortalTelemedicina.DomainService
{
    public class OrderDomainService : IOrderDomainService
    {
        private ApplicationContext context;

        public OrderDomainService(ApplicationContext _context)
        {
            context = _context;
        }

        public List<Order> Get(int? orderId, int? userId, DateTime? startDate, DateTime? endDate)
        {
            var ordersQuery = context.Orders.AsQueryable();

            if (orderId.HasValue)
            {
                ordersQuery = ordersQuery.Where(x => x.OrderId == orderId.Value);
            }
            else
            {
                if (userId.HasValue)
                    ordersQuery = ordersQuery.Where(x => x.UserId == userId.Value);

                if (startDate.HasValue && endDate.HasValue)
                    ordersQuery = ordersQuery.Where(x => x.CreationDate >= startDate && x.CreationDate <= endDate);
            }

            // get order items -- can be updated to get automatically
            var ordersList = ordersQuery.ToList();

            foreach (var o in ordersList)
            {
                o.OrderItems = context.OrderItems.Where(x => x.OrderId == o.OrderId).ToList();
            }

#warning implement sort

            return ordersList;
        }

        public bool Create(Order order)
        {
            // check if products was passed and if the user exists in the database
            if (order.OrderItems.Count > 0 && context.Users.Any(x => x.UserId == order.UserId))
            {
                order.CreationDate = DateTime.Now;

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
                    context.Orders.Add(newOrder);

                    return context.SaveChanges() > 0;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }
}

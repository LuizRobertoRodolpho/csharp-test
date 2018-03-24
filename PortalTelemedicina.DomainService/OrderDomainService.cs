using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository.Entities;
using System;
using System.Collections.Generic;

namespace PortalTelemedicina.DomainService
{
    public class OrderDomainService : IOrderDomainService
    {
        public bool Create(int userId, int[] productIds)
        {
            throw new NotImplementedException();
        }

        public Order Get(int orderId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetByProductId(int productId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}

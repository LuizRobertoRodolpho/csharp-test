using PortalTelemedicina.Repository.Entities;
using System;
using System.Collections.Generic;

namespace PortalTelemedicina.DomainService.Interfaces
{
    public interface IOrderDomainService
    {
        /// <summary>
        /// Search for a order given an id.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Returns the order object.</returns>
        List<Order> Get(int? orderId, int? userId, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Create a new order with the associated products. Product ids that does not exist in the database will be skiped.
        /// </summary>
        /// <returns>Returns true if the order was succesfully created.</returns>
        bool Create(Order order);
    }
}

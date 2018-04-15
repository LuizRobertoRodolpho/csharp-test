using PortalTelemedicina.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTelemedicina.DomainService.Interfaces
{
    public interface IOrderDomainService
    {
        /// <summary>
        /// Search for orders given the parameters as filters.
        /// </summary>
        /// <param name="orderId">Order given a specific id.</param>
        /// <param name="userId">Orders given a user id.</param>
        /// <param name="startDate">Orders given a initial date.</param>
        /// <param name="endDate">Orders given a end date.</param>
        /// <param name="minTotal">Orders with a minimum total value.</param>
        /// <param name="maxTotal">Orders with a maximum total value.</param>
        /// <returns>Returns a list of orders.</returns>
        Task<List<Order>> Get(int? orderId, int? userId, DateTime? startDate, DateTime? endDate, decimal? minTotal, decimal? maxTotal);

        /// <summary>
        /// Create a new order with the associated products. Product ids that does not exist are reported within an exception.
        /// </summary>
        /// <returns>Returns true if the order was succesfully created.</returns>
        Task<bool> Create(Order order);
    }
}

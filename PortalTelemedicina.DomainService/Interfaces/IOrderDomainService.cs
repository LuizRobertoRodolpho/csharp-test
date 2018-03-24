using PortalTelemedicina.Repository.Entities;
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
        Order Get(int orderId);

        /// <summary>
        /// Search for orders from a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Returns a list of orders matching the user id.</returns>
        IEnumerable<Order> GetByUserId(int userId);

        /// <summary>
        /// Search for orders contains the given product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Returns a list of orders matching the product id.</returns>
        IEnumerable<Order> GetByProductId(int productId);

        /// <summary>
        /// Create a new order with the associated products. Product ids that does not exist in the database will be skiped.
        /// </summary>
        /// <param name="userId">User associated with the order.</param>
        /// <param name="productIds">An array of the product ids included in the order.</param>
        /// <returns>Returns true if the order was succesfully created.</returns>
        bool Create(int userId, int[] productIds);
    }
}

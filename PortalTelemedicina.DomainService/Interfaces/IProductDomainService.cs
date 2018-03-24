using PortalTelemedicina.Repository.Entities;
using System;
using System.Collections.Generic;

namespace PortalTelemedicina.DomainService.Interfaces
{
    public interface IProductDomainService
    {
        /// <summary>
        /// Search for products given the optional fields.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        IEnumerable<Product> Get(string name, string description, decimal? price,
            DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <returns>Returns true if the product was succesfully created.</returns>
        bool Create(string name, string description, decimal price);

        /// <summary>
        /// Update a existing product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <returns>Returns true if the product was succesfully updated.</returns>
        bool Update(int productId, string name, string description, decimal? price);
    }
}

using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository.Entities;
using System;
using System.Collections.Generic;

namespace PortalTelemedicina.DomainService
{
    public class ProductDomainService : IProductDomainService
    {
        public bool Create(string name, string description, decimal price)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Get(string name, string description, decimal? price, DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public bool Update(int productId, string name, string description, decimal? price)
        {
            throw new NotImplementedException();
        }
    }
}

﻿using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository;
using PortalTelemedicina.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortalTelemedicina.DomainService
{
    public class ProductDomainService : IProductDomainService
    {
        private ApplicationContext context;

        public ProductDomainService(ApplicationContext _context)
        {
            context = _context;
        }

        public IEnumerable<Product> Get(string name, string description, decimal? price,
            DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var productsQuery = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                    productsQuery = productsQuery.Where(x => x.Name == name);

                if (!string.IsNullOrEmpty(description))
                    productsQuery = productsQuery.Where(x => x.Description.Contains(description));

                if (startDate.HasValue && endDate.HasValue)
                    productsQuery = productsQuery.Where(x => x.CreationDate >= startDate && x.CreationDate <= endDate);

                if (price.HasValue)
                    productsQuery = productsQuery.Where(x => x.Price == price.Value);

#warning implement sort

                return productsQuery;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to save the product. " + ex.Message);
                throw ex;
            }
        }

        public void Create(string name, string description, decimal price)
        {
            try
            {
                context.Products.Add(new Product
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    CreationDate = DateTime.Now
                });

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to save the product. " + ex.Message);
                throw ex;
            }
        }

        public bool Update(int productId, string name, string description, decimal? price)
        {
            try
            {
                var product = context.Products.Where(p => p.ProductId == productId).FirstOrDefault();

                if (product != null)
                {
                    if (!string.IsNullOrEmpty(name))
                        product.Name = name;

                    if (!string.IsNullOrEmpty(description))
                        product.Description = description;

                    if (price.HasValue)
                        product.Price = price.Value;

                    context.Products.Update(product);

                    return context.SaveChanges() > 0;
                }

                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to update the product. " + ex.Message);
                return false;
            }
        }
    }
}
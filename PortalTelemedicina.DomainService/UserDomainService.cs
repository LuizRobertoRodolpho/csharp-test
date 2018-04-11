using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository;
using PortalTelemedicina.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortalTelemedicina.DomainService
{
    public class UserDomainService : IUserDomainService
    {
        private ApplicationContext context;

        public UserDomainService(ApplicationContext _context)
        {
            context = _context;
        }

        public IEnumerable<User> Get(string username, string displayname,
            DateTime? startDate, DateTime? endDate, string email)
        {
            var userQuery = context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(username))
                userQuery = userQuery.Where(x => x.UserName == username);

            if (!string.IsNullOrEmpty(displayname))
                userQuery = userQuery.Where(x => x.DisplayName.Contains(displayname));

            if (startDate.HasValue && endDate.HasValue)
                userQuery = userQuery.Where(x => x.CreationDate >= startDate && x.CreationDate <= endDate);

            if (!string.IsNullOrEmpty(email))
                userQuery = userQuery.Where(x => x.Email == email);

#warning implement sort

            return userQuery;
        }

        public bool Create(User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.DisplayName) || string.IsNullOrEmpty(user.UserName) ||
                    string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email))
                    throw new Exception("Invalid fields.");

                if (context.Users.Where(x => x.UserName == user.UserName).Any())
                {
                    throw new Exception("Username already in use.");
                }
                else
                {
                    user.CreationDate = DateTime.Now;
                    context.Users.Add(user);
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to save the product. " + ex.Message);

                throw ex;
            }
        }

        public bool Get(User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                    return false;

                return context.Users.Where(x => x.UserName == user.UserName && x.Password == user.Password).Any();
            }
            catch
            {
                throw new Exception("Unable to find the user.");
            }
        }
    }
}

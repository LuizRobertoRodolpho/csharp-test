using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository.Entities;
using System;
using System.Collections.Generic;

namespace PortalTelemedicina.DomainService
{
    public class UserDomainService : IUserDomainService
    {
        public string Create(string username, string displayname, string email, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Get(string username, string displayname, DateTime? startDate, DateTime? endDate, string email)
        {
            throw new NotImplementedException();
        }

        public User Get(string uesrname, string password)
        {
            throw new NotImplementedException();
        }
    }
}

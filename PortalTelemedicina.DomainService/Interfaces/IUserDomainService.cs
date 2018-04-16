using PortalTelemedicina.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTelemedicina.DomainService.Interfaces
{
    public interface IUserDomainService
    {
        /// <summary>
        /// Search for users given the optional fields.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="displayname"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="email"></param>
        /// <param name="orderBy">Column to sort by</param>
        /// <param name="orderType">Sort type asc for ascending, desc for descending</param>
        /// <returns>Returns a list of users.</returns>
        Task<List<User>> Get(string username, string displayname, DateTime? startDate, DateTime? endDate, string email, string orderBy, string orderType);

        /// <summary>
        /// Search for a user the given a username and password.
        /// </summary>
        /// <param name="uesrname"></param>
        /// <param name="password"></param>
        /// <returns>Returns true if user was found.</returns>
        Task<bool> Get(string username, string password);

        /// <summary>
        /// Attemp to create a user in the database.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="displayname"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Returns true if the user was succesfully created, otherwise, throws a error message.</returns>
        Task<bool> Create(User user);
    }
}

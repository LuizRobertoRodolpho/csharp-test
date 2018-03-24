using PortalTelemedicina.Repository.Entities;
using System;
using System.Collections.Generic;

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
        /// <returns></returns>
        IEnumerable<User> Get(string username, string displayname,
            DateTime? startDate, DateTime? endDate, string email);

        /// <summary>
        /// Search for the given user and password.
        /// </summary>
        /// <param name="uesrname"></param>
        /// <param name="password"></param>
        /// <returns>Returns the user object.</returns>
        User Get(string uesrname, string password);

        /// <summary>
        /// Attemp to create a user in the database.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="displayname"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Returns null if the user was succesfully created, otherwise, return the error message.</returns>
        string Create(string username, string displayname, string email, string password);
    }
}

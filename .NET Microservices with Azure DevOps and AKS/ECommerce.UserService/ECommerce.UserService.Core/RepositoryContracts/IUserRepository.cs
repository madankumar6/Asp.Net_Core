using ECommerce.UserService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UserService.Core.RepositoryContracts
{
    public interface IUserRepository
    {
        /// <summary>
        /// Method to add user to the data store
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ApplicationUser> AddUser(ApplicationUser user);
        /// <summary>
        /// Method to get a user details using email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ApplicationUser> GetUserByEmailAndPassword(string email, string password);
    }
}

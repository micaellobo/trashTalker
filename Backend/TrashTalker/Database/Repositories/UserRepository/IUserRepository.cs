using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrashTalker.Helpers;
using TrashTalker.Models;

namespace TrashTalker.Database.Repositories.UserRepository
{
    /// <summary>
    /// This interface represents a User Repository that contains all the necessary methods for User management.
    /// This Repository is also important because it establishes a direct connection to the SQL database.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Returns all the existing <see cref="User"/>.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}" /> of <see cref="User" /></returns>
        Task<IEnumerable<User>> getUsers();

        /// <summary>
        /// Returns a specific <see cref="User"/> by its id.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> of the <see cref="User"/> to find</param>
        /// <returns>The chosen <see cref="User" /></returns>
        Task<User> getUser(Guid id);
        
        /// <summary>
        /// Returns a specific <see cref="User"/> by its username.
        /// </summary>
        /// <param name="username">The <see cref="String"/> of the <see cref="User"/> to find</param>
        /// <returns>The chosen <see cref="User" /></returns>
        Task<User> getUserByUsername(String username);

        /// <summary>
        /// Gets an specific <see cref="User"/> given an specific <see cref="Login"/>
        /// </summary>
        /// <param name="login">The login to find the <see cref="User"/></param>
        /// <returns>The <see cref="User"/> for the Login </returns>
        Task<User> getUserLogin(Login login);

        /// <summary>
        /// Adds a <see cref="User"/> to the database.
        /// </summary>
        /// <param name="user">to be added</param>
        /// <returns>The added <see cref="User"/></returns>
        Task<User> addUser(User user);

        /// <summary>
        /// This method updates the data of a <see cref="User"/> in the database.
        /// </summary>
        /// <param name="user"><see cref="User"/> to be updated</param>
        /// <returns>The updated <see cref="User" /></returns>
        Task<User> updateUser(User user);

        /// <summary>
        /// This method disables an specific <see cref="User"/>
        /// </summary>
        /// <param name="id"> The <see cref="Guid"/> of the <see cref="User"/> to disable </param>
        /// <returns>The disabled <see cref="User" /></returns>
        Task<User> disableUser(Guid id);
    }
}
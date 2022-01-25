using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrashTalker.Helpers;
using TrashTalker.Models;
using TrashTalker.Models.Enumerations;
using TrashTalker.Services;

namespace TrashTalker.Database.Repositories.UserRepository
{
    /// <summary>
    /// This Class represents a User Repository that contains all the necessary methods for <see cref="User"/> management.
    /// This Repository is also important because it establishes a direct connection to the SQL database.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// A DbContext instance that represents a session with the database that can be used to query and save
        /// instances of your entities.
        /// </summary>
        private readonly DatabaseContext _dbContext;

        /// <summary>
        /// Constructor method for the User repository.
        /// </summary>
        /// <param name="databaseContext"> <see cref="DatabaseContext"/> of database </param>
        public UserRepository(DatabaseContext databaseContext)
        {
            this._dbContext = databaseContext;
        }

        /// <inheritdoc/>
        public async Task<User> addUser(User user)
        {
            var personToAdd = await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();

            return personToAdd.Entity;
        }

        /// <inheritdoc/>
        public async Task<User> getUser(Guid id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.id == id);
        }
        
        /// <inheritdoc/>
        public async Task<User> getUserByUsername(String username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.username.Equals(username));
        }

        /// <inheritdoc/>
        public async Task<User> getUserLogin(Login login)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.username == login.username);

            if (user is null)
                return null;
            return PasswordEncrypterService.comparePasswords(login.password, user.password) ? user : null;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<User>> getUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<User> updateUser(User user)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(userDb => userDb.id == user.id);

            if (result is null) return null;

            result.password = user.password;
            result.firstName = user.firstName;
            result.lastName = user.lastName;
            result.email = user.email;
            result.street = user.street;
            result.city = user.city;
            result.zipCode = user.zipCode;
            result.country = user.country;
            await _dbContext.SaveChangesAsync();
            return result;
        }

        /// <inheritdoc/>
        public async Task<User> disableUser(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.id == id);

            if (user is null) return null;
            user.status = Status.INACTIVE;
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
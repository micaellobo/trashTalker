using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrashTalker.Database.Repositories.UserRepository;
using TrashTalker.Dto.User;
using TrashTalker.Helpers;
using TrashTalker.Models;
using TrashTalker.Models.Enumerations;
using TrashTalker.Services;

namespace TrashTalker.Controllers
{
    /// <summary>
    /// This controller manage users.
    /// </summary>
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// This constructor inject the user repository to be use by the user controller.
        /// </summary>
        /// <param name="userRepository"></param>
        public UserController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// Returns all users,
        /// </summary>
        /// <returns><see cref="UserDTO"/></returns>
        // GET: api/values
        [HttpGet]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> getUsersAsync()
        {
            try
            {
                var users = (await _userRepository.getUsers()).Where(user => user.role != Role.ADMIN);

                return Ok(users.Select(user => user.asDTO()));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Return the specific user of the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="UserDTO"/></returns>
        // GET api/values/5
        // GET api/values/5
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<ActionResult<UserDTO>> getUser(Guid id)
        {
            try
            {
                var user = await _userRepository.getUser(id);
                if (user == null)
                    return NotFound();

                return user.asDTO();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Return the specific user of the given username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns><see cref="UserDTO"/></returns>
        // GET api/values/5
        // GET api/values/5
        [HttpGet("{username}")]
        //[Authorize(Roles = "MANAGER | Employee")]
        public async Task<ActionResult<UserDTO>> getUserByUsername(String username)
        {
            try
            {
                var user = await _userRepository.getUserByUsername(username);
                if (user == null)
                    return NotFound();

                return user.asDTO();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="createUserDto"></param>
        /// <returns>Created <see cref="UserDTO"/></returns>
        // POST api/values
        [HttpPost]
        [Authorize(Roles = "MANAGER,ADMIN")]
        public async Task<ActionResult<UserDTO>> addUser([FromBody] CreateUserDTO createUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (createUserDto == null)
                    return BadRequest();

                if (createUserDto.role.toRole() == Role.MANAGER && getCurrentUserRole() != Role.ADMIN)
                    return BadRequest("Only Admin can create a user with profile Manager");

                var userDb = (await _userRepository.getUsers()).FirstOrDefault(userDb => userDb.username.Equals(createUserDto.username));

                if (userDb is not null)
                    return BadRequest($"The user with username {createUserDto.username} already exists!");

                createUserDto.password = PasswordEncrypterService.encryptPassword(createUserDto.password);

                var user = await _userRepository.addUser(createUserDto.asUser());

                return CreatedAtAction(nameof(getUser), new {user.id}, user.asDTO());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Updates the specific user of the given id.
        /// </summary>
        /// <param name="updateUserDto"></param>
        /// <param name="id"></param>
        /// <returns><see cref="UpdateUserDTO"/></returns>
        // PUT api/values/5
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<IActionResult> updateUser([FromBody] UpdateUserDTO updateUserDto, Guid id)
        {
            try
            {
                var user = await _userRepository.getUser(id);
                if (user == null)
                    return NotFound();

                if (updateUserDto.password is not null)
                    user.password = PasswordEncrypterService.encryptPassword(updateUserDto.password);

                user.email = updateUserDto.email;
                user.street = updateUserDto.street;
                user.city = updateUserDto.city;
                user.zipCode = updateUserDto.zipCode;
                user.country = updateUserDto.country;

                var updatedUser = await _userRepository.updateUser(user);
                return Ok(updatedUser.asDTO());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// /// Updates an <see cref="User"/> that's is logged in
        /// </summary>
        /// <param name="updateUserDto"></param>
        [HttpPut]
        [Authorize(Roles = "EMPLOYEE")]
        public async Task<IActionResult> updateUserLoggedIn([FromBody] UpdateUserDTO updateUserDto)
        {
            try
            {
                return await updateUser(updateUserDto, getCurrentUserId());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Disable the specifc user of the given id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<IActionResult> disabeUser(Guid id)
        {
            try
            {
                var user = await _userRepository.getUser(id);
                if (user == null)
                    return NotFound();

                await _userRepository.disableUser(user.id);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        private Guid getCurrentUserId()
        {
            var value = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(value, out var guid);
            return guid;
        }

        private Role getCurrentUserRole()
        {
            var roleToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var role = roleToken.toRole();
            return role;
        }
    }
}
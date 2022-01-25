using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrashTalker.Database.Repositories;
using TrashTalker.Database.Repositories.UserRepository;
using TrashTalker.Helpers;
using TrashTalker.Models.Enumerations;
using TrashTalker.Services;


namespace TrashTalker.Controllers
{
    /// <summary>
    /// This controller allows rest api autentication.
    /// </summary>
    [Route("api/v1")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// This constructor injects the repository to be used by the AuthController.
        /// </summary>
        /// <param name="userRepository"></param>
        public AuthController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// This method allows the user to start session in rest api.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> loginAsync([FromBody] Login login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userDb = await _userRepository.getUserLogin(login);

                if (userDb == null)
                    return NotFound(new {message = "Invalid Username or Password"});

                var token = TokenService.GenerateToken(userDb);

                Response.Headers.Add("Authorization", token);
                // Response.Cookies.Append("Authorization", token, new CookieOptions { HttpOnly = true });

                if (userDb.status == Status.INACTIVE)
                    return NotFound(new {message = "Your account is inactive"});

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// This method allows the user to start session in rest api.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("loginMobile")]
        public async Task<IActionResult> loginMobileAsync([FromBody] Login login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userDb = await _userRepository.getUserLogin(login);

                if (userDb == null)
                    return NotFound(new {message = "Invalid Username or Password"});

                if (!userDb.role.Equals(Role.EMPLOYEE))
                    return BadRequest("Only employee has access to the application.");

                var token = TokenService.GenerateToken(userDb);

                Response.Headers.Add("Authorization", token);
                // Response.Cookies.Append("Authorization", token, new CookieOptions { HttpOnly = true });

                if (userDb.status == Status.INACTIVE)
                    return NotFound(new {message = "Your account is inactive"});

                return Ok();
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
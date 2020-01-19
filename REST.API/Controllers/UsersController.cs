using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using REST.API.Controllers.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace REST.API.Controllers
{
    /// <summary>
    /// Users
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [SwaggerTag("User controller with PageSize PageNumber")]
    public class UsersController : ControllerBase
    {
        private static IEnumerable<User> _usersList = Model.User.GetSampleUsers();

        /// <summary>
        /// Return all users
        /// </summary>
        /// <returns>Users array</returns>
        /// <remarks>Returns all users</remarks>
        [HttpGet("", Name = "GetAllUsersPageSize")]
        [SwaggerOperation(Summary = "Get all users", Description = "Returns all users")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<User>), Description = "Array of all users")]
        public IActionResult GetAllUsersPageSize([FromQuery] UserFilter userFilter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = _usersList.OrderBy(s=>s.Created).Where(s=>s.Created>=userFilter.Created).Take(userFilter.Limit).ToList();

            Response.Headers.Add(new KeyValuePair<string, StringValues>("Count", _usersList.Count().ToString()));

            return Ok(result);
        }
    }

    /// <summary>
    /// User filter
    /// </summary>
    public class UserFilter
    {
        /// <summary>
        /// Number of item on page
        /// </summary>
        [Required]
        public int Limit { get; set; }

        /// <summary>
        /// Filter by created time
        /// </summary>
        [Required]
        public DateTime Created { get; set; }
    }
}
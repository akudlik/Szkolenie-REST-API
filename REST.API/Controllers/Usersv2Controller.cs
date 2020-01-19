using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using REST.API.Controllers.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace REST.API.Controllers
{
    /// <summary>
    /// Users
    /// </summary>
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/Users")]
    [Produces("application/json")]
    [ApiController]
    [SwaggerTag("User controller with PageSize PageNumber")]
    public class Users2Controller : ControllerBase
    {
        private static IEnumerable<User> _usersList = Model.User.GetSampleUsers();

        /// <summary>
        /// Return all users
        /// </summary>
        /// <returns>Users array</returns>
        /// <remarks>Returns all users</remarks>
        [HttpGet("", Name = "GetAllUsersPageSize")]
        [SwaggerOperation(Summary = "Get all users v2", Description = "Returns all users")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<User>), Description = "Array of all users")]
        public IActionResult GetAllUsersPageSize()
        {
          return Ok("Version 2.0");
        }
    }

    
}
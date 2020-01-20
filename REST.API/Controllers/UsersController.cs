using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using REST.API.Controllers.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace REST.API.Controllers
{
    /// <summary>
    /// Users
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml", "application/x-yaml")]
    [ApiController]
    [SwaggerTag("User controller with PageSize PageNumber")]
    public class UsersController : ControllerBase
    {
        public static IEnumerable<User> usersList = Model.User.GetSampleUsers();

        /// <summary>
        /// Return all users
        /// </summary>
        /// <returns>Users array</returns>
        /// <remarks>Returns all users</remarks>
        [HttpGet("", Name = "GetAllUsersPageSize")]
        [SwaggerOperation(Summary = "Get all users", Description = "Returns all users")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<User>), Description = "Array of all users")]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Client, NoStore = false)]
        public IActionResult GetAllUsersPageSize([FromQuery] UserFilter userFilter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var result = usersList.Skip(userFilter.PageNumber * userFilter.PageSize).Take(userFilter.PageSize).ToList();

            if (Request.Headers.Any(s => s.Key == "If-Match"))
            {
                var ifMatch = Request.Headers.FirstOrDefault(s => s.Key == "If-Match").Value.ToList();

                if (!ifMatch.Contains(result.Encrypt()))
                    return StatusCode(412,"Wrong ETag");
            }
            
            Response.Headers.Add(new KeyValuePair<string, StringValues>("Count", usersList.Count().ToString()));

            string ExpireDate = DateTime.UtcNow.AddMinutes(60).ToString("ddd, dd MMM yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            Response.Headers.Add("Expires", ExpireDate + " GMT");
            Response.Headers.Add("ETag", result.Encrypt());

            return Ok(result);
        }
    }

    public static class Sha1Encrypt
    {
        public static string Encrypt(this object obj)
        {
            using (var sha256Hash = SHA256.Create())
            {
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)));

                var builder = new StringBuilder();
                foreach (var bytee in bytes)
                {
                    builder.Append(bytee.ToString("x2"));
                }

                return builder.ToString();
            }
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
        [Range(1, 100)]
        public int PageSize { get; set; }

        /// <summary>
        /// Number of page
        /// </summary>
        [Required]
        public int PageNumber { get; set; }

        /// <summary>
        /// Test
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Test { get; set; }
    }
}
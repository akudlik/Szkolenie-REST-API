using System.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
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


            _usersList=  _usersList.AsQueryable().OrderBy(userFilter.SortBy.Split(","));


            var result = _usersList.Skip(userFilter.PageNumber * userFilter.PageSize).Take(userFilter.PageSize).ToList();

            Response.Headers.Add(new KeyValuePair<string, StringValues>("Count", _usersList.Count().ToString()));

            return Ok(result);
        }
    }

    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IEnumerable<string> sortModels)
        {
            var expression = source.Expression;
            int count = 0;
            foreach (var item in sortModels)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var selector = Expression.PropertyOrField(parameter, item.Replace("-",""));
                var method = item.Contains("-", StringComparison.OrdinalIgnoreCase) ?
                    (count == 0 ? "OrderByDescending" : "ThenByDescending") :
                    (count == 0 ? "OrderBy" : "ThenBy");
                expression = Expression.Call(typeof(Queryable), method,
                    new Type[] { source.ElementType, selector.Type },
                    expression, Expression.Quote(Expression.Lambda(selector, parameter)));
                count++;
            }
            return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
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
        /// Number of page
        /// </summary>
        public string SortBy { get; set; }

        public static IQueryable<T> OrderByPropertyName<T>(IQueryable<T> q, string SortField, bool Ascending)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, SortField);
            var exp = Expression.Lambda(prop, param);
            string method = Ascending ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] {q.ElementType, exp.Body.Type};
            var rs = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(rs);
        }
    }
}
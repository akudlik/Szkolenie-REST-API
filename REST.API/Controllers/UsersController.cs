﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LhsBracketParser;
using LhsBracketParser.LinqProvider;
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
        private IEnumerable<User> _usersList = Model.User.GetSampleUsers();

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

            var evaluator = new LinqExpressionEvaluator<User>();

            if (!string.IsNullOrEmpty(userFilter?.Filter))
            {
                var predicate = evaluator.Evaluate(userFilter?.Filter);
                _usersList = _usersList.Where(predicate);
            }

            var result = _usersList.Skip(userFilter.PageNumber * userFilter.PageSize).Take(userFilter.PageSize).ToList();

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
        [Range(1, 100)]
        public int PageSize { get; set; }

        /// <summary>
        /// Number of page
        /// </summary>
        [Required]
        public int PageNumber { get; set; }

        /// <summary>
        /// Filter in LHS Standard https://github.com/Jalalx/LhsBracketParser
        /// </summary>
        public string Filter { get; set; }
    }
}
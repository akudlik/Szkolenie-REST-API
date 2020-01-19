using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace REST.API.Controllers
{
    /// <summary>
    /// Test API
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [SwaggerTag("Value controller gives access to adding, removing and updating value objects")]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Return one value
        /// </summary>
        /// <returns>Single value</returns>
        /// <param name="id">Value identifVier</param>
        [HttpGet("{id:int}", Name = "GetValueById")]
        [SwaggerOperation(Summary = "Get value by given id", Description = "Returns value by given identifier")]
        [SwaggerResponse(200, Type = typeof(string), Description = "Array of all values")]
        [SwaggerResponse(400, Type = typeof(string), Description = "Invalid identifier")]
        [SwaggerResponse(404, Type = typeof(string), Description = "Not found value of identifier")]
        public IActionResult Get([Required] int id)
        {
            return Ok("value");
        }

        /// <summary>
        /// Return all values
        /// </summary>
        /// <returns>Values array</returns>
        /// <remarks>Returns all values</remarks>
        [HttpGet("", Name = "GetAllValues")]
        [SwaggerOperation(Summary = "Get all values", Description = "Returns all values from data base")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<string>), Description = "Array of all values")]
        public IActionResult GetAllValues()
        {
            return Ok(new[] {"value1", "value2"});
        }

        /// <summary>
        /// Returns values by filter
        /// </summary>
        /// <returns>Values array</returns>
        /// <param name="filter">Value filter object</param>
        [HttpGet("ByFilter", Name = "GetValuesByFilter")]
        [SwaggerOperation(Summary = "Get values by filter", Description = "Returns values that match given filter")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<ValueObject>), Description = "Returns array of values")]
        [SwaggerResponse(400, Type = typeof(string), Description = "Wrong filter given")]
        public IActionResult GetByFilter([FromQuery] Filter filter)
        {
            return Ok(new[]
            {
                new ValueObject() {Id = 1, Value = "Value1"},
                new ValueObject() {Id = 2, Value = "Value2"}
            });
        }

        /// <summary>
        /// Add value
        /// </summary>
        /// <returns>Status</returns>
        /// <param name="value">Value to add</param>
        [HttpPost("", Name = "Post")]
        [SwaggerOperation(Summary = "Add value", Description = "Add given value to data base")]
        [SwaggerResponse(201, Type = typeof(string), Description = "New value object created")]
        [SwaggerResponse(400, Type = typeof(string), Description = "Incorrect value given")]
        public IActionResult Post([FromBody] string value)
        {
            return Created("", "");
        }

        /// <summary>
        /// Edit value and returns created object
        /// </summary>
        /// <returns>Status</returns>
        /// <param name="id">ID of value</param>
        /// <param name="value">Changed value</param>
        [HttpPut("{id:int}", Name = "Put")]
        [SwaggerOperation(Summary = "Update value of id", Description = "Update value of given identifier with given value value")]
        [SwaggerResponse(200, Type = typeof(ValueObject), Description = "Value object updated")]
        [SwaggerResponse(400, Type = typeof(string), Description = "Value was incorrect")]
        [SwaggerResponse(404, Type = typeof(string), Description = "Not found value of given identifier")]
        public IActionResult Put([Required] int id, [FromBody] string value)
        {
            return Ok(new ValueObject() {Id = 1, Value = "Value"});
        }

        /// <summary>
        /// Remove value
        /// </summary>
        /// <returns>Status</returns>
        /// <param name="id">ID of value</param>
        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Delete value", Description = "Remove value of given identifier from data base")]
        [SwaggerResponse(204, Type = typeof(void), Description = "Value deleted")]
        [SwaggerResponse(404, Type = typeof(string), Description = "Not found value of given identifier")]
        public IActionResult Delete([Required] int id)
        {
            return NoContent();
        }
    }

    /// <summary>
    /// Value filter
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Identifier of value object
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Value of value object
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// Value Object
    /// </summary>
    public class ValueObject
    {
        /// <summary>
        /// Identifier of value object
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Value of value object
        /// </summary>
        public string Value { get; set; }
    }
}
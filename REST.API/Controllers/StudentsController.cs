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
using REST.Domain.Service;
using Swashbuckle.AspNetCore.Annotations;

namespace REST.API.Controllers
{
    /// <summary>
    /// Students
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml", "application/x-yaml")]
    [ApiController]
    [SwaggerTag("Student controller")]
    public class StudentsController : ControllerBase
    {
        private IStudentService _studentService;

        public StudentsController()
        {
            _studentService = new StudentService();
        }

        /// <summary>
        /// Return one studnet
        /// </summary>
        /// <returns>Student</returns>
        /// <remarks>Returns one Student</remarks>
        [HttpGet("{id:int}", Name = "GetStudent")]
        [SwaggerOperation(Summary = "Get student", Description = "Returns one student")]
        [SwaggerResponse(200, Type = typeof(Student), Description = "Student")]
        public IActionResult GetStudent([Required] int id)
        {
            var result = _studentService.GetStudentInfo(id);

            if (result.Successed)
                return Ok(result.Value);

            return NotFound();
        }
        
        /// <summary>
        /// Deletion Student
        /// </summary>
        /// <returns>Status</returns>
        /// <remarks>Status</remarks>
        [HttpPost("{id:int}", Name = "StudentDeletion")]
        [SwaggerOperation(Summary = "Deletion student", Description = "RDeletion student")]
        [SwaggerResponse(204, Type = typeof(void), Description = "Deletion")]
        public IActionResult StudentDeletion([Required] int id)
        {
            var result = _studentService.DeletionStudent(id);

            if (result.Successed)
                return NoContent();

            return NotFound();
        }
    }
}